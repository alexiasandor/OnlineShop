using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Laroa.Infrastructure
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task AddAsync(Review review)
        {
            await _context
               .Reviews
               .AddAsync(review);
        }

        public async Task DeleteAsync(Review review)
        {
           _context
               .Reviews
               .Remove(review);
        }

        public async Task<IList<Review>> GetAllAsync()
        {
            return await _context
                .Reviews
                .ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _context
                .Reviews
                .SingleOrDefaultAsync(r => r.Id == id);
        }
    }
}
