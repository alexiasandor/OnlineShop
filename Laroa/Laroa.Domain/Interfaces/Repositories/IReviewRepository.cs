using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        Task<IList<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int id);
        Task AddAsync(Review review);
        Task DeleteAsync(Review review);
    }
}
