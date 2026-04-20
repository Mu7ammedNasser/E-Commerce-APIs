using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet("user/{userId:alpha}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult<IEnumerable<OrderDto>>>> GetOrdersByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _orderManager.GetOrdersByUserIdAsync(userId!);
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
