using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain.Interfaces.Services
{
    public interface IReviewService
    {
        Task<Review> AddAsync(int prodId, int userId, string comment, string userName);
        Task<Review> UpdateAsync(int id, string comment);
        Task<Review> DeleteAsync(int id);
        Task<Review> GetByIdAsync(int id);
        Task<IList<Review>> GetAllAsync();
    }
}
