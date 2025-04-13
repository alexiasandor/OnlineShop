using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Laroa.Infrastructure
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task AddAsync(Admin admin)
        {
            await _context
                .Admin
                .AddAsync(admin);
        }

        public async Task DeleteAsync(Admin admin)
        {
             _context
                .Admin
                .Remove(admin);

        }

        public async Task<IList<Admin>> GetAllAsync()
        {
          return await _context
                .Admin
                .ToListAsync();
        }

        public async Task<Admin>  GetByIdAsync(int id)
        {
            return await _context
               .Admin
               .SingleOrDefaultAsync(p => p.Id == id);
        }

    }
}
