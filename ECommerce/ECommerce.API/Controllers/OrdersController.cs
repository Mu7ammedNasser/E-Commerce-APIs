using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderManager _orderManager;

        public OrdersController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }
        [HttpPost]
        public async Task<ActionResult<GeneralResult<OrderDto>>> Create([FromBody] CreateOrderDto dto)
        {
            var result = await _orderManager.CreateOrderFromCartAsync(dto);

            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<GeneralResult<IEnumerable<OrderDto>>>> GetOrdersByUserId(string userId)
        {
            var result = await _orderManager.GetOrdersByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<GeneralResult<OrderDto>>> GetById(int orderId)
        {
            var result = await _orderManager.GetOrderByIdAsync(orderId);

            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }




    }
}
