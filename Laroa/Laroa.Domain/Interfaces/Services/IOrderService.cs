using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Order> AddAsync(DateTime OrderDate, int userId);
        Task<Order> UpdateAsync(int OrderId, DateTime? OrderDate, double? Price);
        Task<Order> DeleteAsync(int OrderId);
        Task<Order> GetByIdAsync(int OrderId);
        Task<IList<Order>> GetAllAsync();
        Task<Order> AddProductToOrder(int orderId, int productId);
        Task<Order> RemoveProductFromOrder(int orderId, int productId);
        Task<double> CalculateTotalOrderPrice(int orderId);
    }
}
