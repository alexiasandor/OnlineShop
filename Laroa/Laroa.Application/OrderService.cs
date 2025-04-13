using Laroa.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;

namespace Laroa.Application
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> AddAsync( DateTime orderDate, int userId)
        {
            var order = new Order
            {
            
                OrderDate = orderDate,
                UserId = userId
            };

            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.Save();
            return order;
        }

        public async Task<Order> DeleteAsync(int OrderId)
        {
            var searchedOrder = await _unitOfWork.OrderRepository.GetByIdAsync(OrderId);

            if (searchedOrder == null)
                return null;

            await _unitOfWork.OrderRepository.DeleteAsync(searchedOrder);
            await _unitOfWork.Save();

            return searchedOrder;
        }

        public async Task<IList<Order>> GetAllAsync()
        {
            return await _unitOfWork.OrderRepository.GetAllAsync();
        }

        public async Task<Order> GetByIdAsync(int OrderId)
        {
            return await _unitOfWork.OrderRepository.GetByIdAsync(OrderId);
        }

        public async Task<Order> UpdateAsync(int OrderId, DateTime? OrderDate, double? Price)
        {
            var searchedOrder = await _unitOfWork.OrderRepository.GetByIdAsync(OrderId);

            if (searchedOrder == null)
                return null;

            searchedOrder.OrderDate = OrderDate ?? searchedOrder.OrderDate;
            searchedOrder.Price = Price ?? searchedOrder.Price;

            await _unitOfWork.Save();
            return searchedOrder;
        }


        // o sa am un order id si un product id, le cauti pe fiecare in tabelul ei (am si remove), si apoi la order.product.add si se adauga produsul
        // exte exemplu si de patch !!!!

        public async Task<Order> AddProductToOrder(int orderId, int productId)
        {
            var searchedProduct = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            var searchedOrder = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

            if (searchedOrder == null || searchedProduct == null)
            {
                return null;
            }

            searchedOrder.Products.Add(searchedProduct);
            await CalculateTotalOrderPrice(orderId);
            await _unitOfWork.Save();

            return searchedOrder;
        }

        public async Task<double> CalculateTotalOrderPrice(int orderId)
        {
            var searchedOrder = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (searchedOrder == null)
            {
                return 0;
            }
            double totalPrice = 0;

            foreach(var prduct in searchedOrder.Products){
                totalPrice = totalPrice + prduct.Price;
            }

            await UpdateAsync(orderId, null, totalPrice);

            return totalPrice;
        }

        public async Task<Order> RemoveProductFromOrder(int orderId, int productId)
        {
            var searchedProduct = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            var searchedOrder = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

            if (searchedOrder == null || searchedProduct == null)
            {
                return null;
            }

            searchedOrder.Products.Remove(searchedProduct);
            await _unitOfWork.Save();

            return searchedOrder;
        }
    }
}
