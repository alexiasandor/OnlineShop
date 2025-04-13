using Laroa.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Laroa.Api.Dtos;
using Laroa.Application;

namespace Laroa.Api.Controllers
{
    [Route("api/orders")] //api/Orders
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var orders = await _orderService.GetAllAsync();
            if (orders == null)
            {
                return NotFound();
            }

            var mappedOrders = _mapper.Map<IList<OrderGetDto>>(orders);

            return Ok(mappedOrders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var mappedOrder = _mapper.Map<OrderGetDto>(order);

            return Ok(mappedOrder);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] OrderPostDto orderPostDto)
        {
            var insertedOrder = await _orderService.AddAsync(orderPostDto.OrderDate, orderPostDto.UserId);
            if (insertedOrder == null)
            {
                return BadRequest();
            }
            return Ok(insertedOrder);
        }

        [HttpPost]
        [Route("add-prduct-to-order/{orderId}/{productId}")]
        public async Task<IActionResult> AddProductToOrder(int orderId, int productId)
        {
            var order = await _orderService.AddProductToOrder(orderId, productId);
            if(order == null)
            {
                return NotFound();
            }

            var mappedOrder = _mapper.Map<OrderGetDto>(order);
            return Ok(mappedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedOrder = await _orderService.DeleteAsync(id);
            return Ok(deletedOrder);
        }

        [HttpDelete]
        [Route("delete-product-form-order/{orderId}/{productId}")]
        public async Task<IActionResult> RemoveProductFromOrder(int orderId, int productId)
        {
            var order = await _orderService.RemoveProductFromOrder(orderId, productId);
            if (order == null)
            {
                return NotFound();
            }

            var mappedOrder = _mapper.Map<OrderGetDto>(order);
            return Ok(mappedOrder);
        }

        //http patch pt update
        [HttpPatch]
        public async Task<IActionResult> UpdateOrder(int OrderId, DateTime? OrderDate, double? Price)
        {
            var updatedOrder = await _orderService.UpdateAsync(OrderId, OrderDate, Price);
           
            if (updatedOrder == null)
            {
                return NotFound();
            }

            return Ok(updatedOrder);
        }
    }
}
