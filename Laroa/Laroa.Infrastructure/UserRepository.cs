using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Laroa.Infrastructure
{
    public class UserRepository : IUserRepository
    {
            private readonly ApplicationDbContext _dataContext;

            public UserRepository(ApplicationDbContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task CreateAsync(User user)
            {
                await _dataContext
                    .Users
                    .AddAsync(user);
            }

            public async Task<User?> GetByIdAsync(int id)
            {
                return await _dataContext
                    .Users
                    .SingleOrDefaultAsync(u => u.Id == id);
            }

            public async Task<User?> GetByEmailAsync(string email)
            {
                return await _dataContext
                    .Users
                    .SingleOrDefaultAsync(u => u.Email == email);
            }

            public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
            {
                return await _dataContext
                    .Users
                    .SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
            }

            public async Task DeleteAsync(User user)
            {
                _dataContext
                    .Users
                    .Remove(user);
            }

            public async Task<IList<User>> GetAllClientsAsync()
            {
                return await _dataContext
                    .Users
                    .Where(u => u.Role == UserRole.Client)
                    .ToListAsync();
            }

    }
}
