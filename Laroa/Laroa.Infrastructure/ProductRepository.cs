using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Laroa.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task AddAsync(Product product)
        {
            await _context
                .Products
                .AddAsync(product);
        }

        public Task AddAsync(Admin admin)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Product product)
        {
            _context
                .Products
                .Remove(product);
        }

        public Task DeleteAsync(Admin searchedAdmin)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await _context
                .Products
                .Include(p => p.Category)
                .Include(p => p.ProductImage)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context
                .Products
                .Include(p => p.Category)
                .Include(p => p.ProductImage)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
