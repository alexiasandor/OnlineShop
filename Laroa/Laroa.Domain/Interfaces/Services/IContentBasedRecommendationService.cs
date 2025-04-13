using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain.Interfaces.Services
{
    public interface IContentBasedRecommendationService
    {
        Task<List<Product>> GetContentBasedRecommendationsAsync(int productId);
    }
}
