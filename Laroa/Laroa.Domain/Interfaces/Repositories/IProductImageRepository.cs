using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain.Interfaces.Repositories
{
    public interface IProductImageRepository
    {
        Task Create(ProductImage productImage);
        Task<ProductImage> GetByProductId(int productId);
        Task Delete(ProductImage productImage);
    }
}
