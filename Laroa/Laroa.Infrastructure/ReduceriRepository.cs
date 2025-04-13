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
    public class ReduceriRepository : IReduceriRepository
    {
        private readonly ApplicationDbContext _context;

        public ReduceriRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

         public async Task AddAsync(Reduceri reduceri)
        {
           await _context
                .Reduceri
                .AddAsync(reduceri);
        }

        public async Task DeleteAsync(Reduceri reduceri)
        {
                _context.
                 Reduceri
                .Remove(reduceri);
        }

        public async Task<IList<Reduceri>> GetAllAsync()
        {
            return await _context
                .Reduceri
                .Include(r => r.Products)
                .ToListAsync();
        }

        public async Task<Reduceri> GetByIdAsync(int id)
        {
            return await _context
               .Reduceri
               .Include(r => r.Products)
               .SingleOrDefaultAsync(r => r.Id == id);
        }
    }
}
