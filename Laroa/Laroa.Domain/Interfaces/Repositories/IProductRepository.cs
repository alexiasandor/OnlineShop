namespace Laroa.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task DeleteAsync(Product product);
        Task AddAsync(Admin admin);
        Task DeleteAsync(Admin searchedAdmin);
    }
}
