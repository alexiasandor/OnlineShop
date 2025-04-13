using Azure.Core;
using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Laroa.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Laroa.Application
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageService _imageStorageService;

        public ProductService(IUnitOfWork unitOfWork, IImageStorageService imageStorageService)
        {
            _unitOfWork = unitOfWork;
            _imageStorageService = imageStorageService;
        }

        public async Task<Product> AddAsync(string name, string description, int categoryId, double price, int stock)
        {
            var product = new Product
            {
                Name = name,
                Description = description,
                CategoryId = categoryId,  // Set the CategoryId here,
                Price = price,
                PriceR = price,
                Stock = stock
            };

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.Save();

            return product;
        }


        public async Task<Product> DeleteAsync(int id)
        {
            var searchedProduct = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (searchedProduct == null)
                return null;

            await _unitOfWork.ProductRepository.DeleteAsync(searchedProduct);
            await _unitOfWork.Save();

            return searchedProduct;
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task<Product> UpdateAsync(int id, string name, string description, double? price, int? stock)
        // float? price, int? stock
        {
            var searchedProduct = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (searchedProduct == null)
                return null;

            searchedProduct.Name = name ?? searchedProduct.Name;
            searchedProduct.Description = description ?? searchedProduct.Description;
            searchedProduct.Price = price ?? searchedProduct.Price;
            searchedProduct.Stock = stock ?? searchedProduct.Stock;

            await _unitOfWork.Save();

            return searchedProduct;
        }

        public async Task<string> AddImageToProductAsync(int productId, IFormFile File, string ContainerName)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return null;
            }

            var fileName = product.Id.ToString() + product.Name;
            var imageUrl = await _imageStorageService.UploadImage(fileName, File, ContainerName);

            var productImage = new ProductImage
            {
                ProductId = productId,
                StorageImageUrl = imageUrl,
                Product = product
            };

            await _unitOfWork.ProductImageRepository.Create(productImage);
            await _unitOfWork.Save();

            return imageUrl;
        }

        
    }
}