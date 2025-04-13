using Microsoft.AspNetCore.Http;

namespace Laroa.Domain.Interfaces.Services
{
    public interface IProductService
    {

        Task<Product> AddAsync(string name, string description, int categoryId, double price, int stock);
        Task<Product> UpdateAsync(int id, string name, string description, double? price, int? stock);
        Task<Product> DeleteAsync(int id);
        Task<Product> GetByIdAsync(int id);
        Task<IList<Product>> GetAllAsync();
        Task<string> AddImageToProductAsync(int productId, IFormFile File, string ContainerName);
    }
}
