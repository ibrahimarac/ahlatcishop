using Ahlatci.Shop.Application.Models.Dtos.Orders;
using Ahlatci.Shop.Application.Models.RequestModels.Orders;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Application.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ahlatci.Shop.Api.Controllers
{

    [ApiController]
    [Route("order")]    
    [Authorize("User")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("getByCustomer/{id:int?}")]
        [AllowAnonymous]
        public async Task<ActionResult<Result<List<OrderDto>>>> GetOrdersByCustomer(int? customerId)
        {
            var categories = await _orderService.GetOrdersByCustomer(new GetOrdersByCustomerVM { CustomerId = customerId});
            return Ok(categories);
        }


        [HttpPost("create")]
        public async Task<ActionResult<Result<int>>> CreateOrder(CreateOrderVM createOrderVM)
        {
            var orderId = await _orderService.CreateOrder(createOrderVM);
            return Ok(orderId);
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<Result<int>>> UpdateOrder(int id, UpdateOrderVM updateOrderVM)
        {
            if(id != updateOrderVM.OrderId)
            {
                return BadRequest();
            }
            var orderId = await _orderService.UpdateOrder(updateOrderVM);
            return Ok(orderId);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<Result<int>>> DeleteProduct(int id)
        {
            var orderId = await _orderService.DeleteOrder(new DeleteOrderVM { OrderId = id});
            return Ok(orderId);
        }

    }
}

