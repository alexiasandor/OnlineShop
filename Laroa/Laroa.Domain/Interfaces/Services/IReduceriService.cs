using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain.Interfaces.Services
{
    public interface IReduceriService
    {
        Task<Reduceri> AddAsync(DateTime Perioada, float Procent, string tip);
        Task<Reduceri> UpdateAsync(int reduceriId, DateTime? Perioada, float Procent, string tip);

        Task<Reduceri> DeleteAsync(int reduceriId);
        Task<Reduceri> GetByIdAsync(int reduceriId);
        Task<IList<Reduceri>> GetAllAsync();
        Task<Reduceri> AddReduceriToProduct(int reduceriId, int productId);
        Task<Reduceri> RemoveProductFromReduceri(int reduceriId, int productId);
        
    }
}
