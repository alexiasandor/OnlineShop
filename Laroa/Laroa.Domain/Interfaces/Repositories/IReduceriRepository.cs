using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain.Interfaces.Repositories
{
    public interface IReduceriRepository
    {
        Task<IList<Reduceri>> GetAllAsync();
        Task<Reduceri> GetByIdAsync(int id);
        Task AddAsync(Reduceri reduceri);
        Task DeleteAsync(Reduceri reduceri);
    }
}
