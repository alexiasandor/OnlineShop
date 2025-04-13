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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task AddAsync(Order order)
        {
            await _context
                .Orders
                .AddAsync(order);
        }

        public async Task DeleteAsync(Order order)
        {
            _context
                 .Orders
                 .Remove(order);
        }

        public async Task<IList<Order>> GetAllAsync()
        {
            return await _context
                .Orders
                .Include(o => o.Products)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context
                .Orders
                .Include(o => o.Products)
                .SingleOrDefaultAsync(o => o.Id == id);
        }
    }
}
