using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByEmailAndPasswordAsync(string email, string password);
        Task<IList<User>> GetAllClientsAsync();
        Task DeleteAsync(User user);
    }
}
