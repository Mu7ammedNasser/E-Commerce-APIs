using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartManager _cartManager;
        public CartsController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<CartDto>>>> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _cartManager.GetCartByUserIdAsync(userId!);
            if (!result.IsSuccess)
            {
                return NotFound($"No cart found for user with id {userId}.");
            }
            return Ok(result);
        }

        [HttpPost("items")]
        public async Task<ActionResult<GeneralResult>> AddItemToCart( [FromBody] AddToCartDto cartItem)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _cartManager.AddToCartAsync(userId!, cartItem);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete("items/{productId:int}")]
        public async Task<ActionResult<GeneralResult>> RemoveItemFromCart( int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _cartManager.RemoveFromCartAsync(userId!, productId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPut("items")]
        public async Task<ActionResult<GeneralResult<CartDto>>> UpdateQuantity( [FromBody] UpdateCartItemQuantityDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _cartManager.UpdateQuantityAsync(userId, dto);

            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("Clear")]
        public async Task<ActionResult<GeneralResult>> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _cartManager.ClearCartAsync(userId!);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
