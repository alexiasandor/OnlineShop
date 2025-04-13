using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Infrastructure
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(ProductImage productImage)
        {
            await _context.ProductImages.AddAsync(productImage);
        }

        public async Task Delete(ProductImage productImage)
        {
             _context.ProductImages.Remove(productImage);
        }

        public async Task<ProductImage> GetByProductId(int productId)
        {
            return await _context.ProductImages.SingleOrDefaultAsync(productImage => productImage.ProductId == productId);
        }
    }
}
