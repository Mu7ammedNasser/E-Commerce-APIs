using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartManager _cartManager;
        public CartsController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet("{userId:string}")]
        public async Task<ActionResult<GeneralResult<IEnumerable<CartDto>>>> GetCart(string userId)
        {
            var result = await _cartManager.GetCartByUserIdAsync(userId);
            if (!result.IsSuccess)
            {
                return NotFound($"No cart found for user with id {userId}.");
            }
            return Ok(result);
        }

        [HttpPost("{userId:string}/items")]
        public async Task<ActionResult<GeneralResult>> AddItemToCart(string userId, [FromBody] AddToCartDto cartItem)
        {
            var result = await _cartManager.AddToCartAsync(userId, cartItem);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete("{userId:string}/items/{productId:int}")]
        public async Task<ActionResult<GeneralResult>> RemoveItemFromCart(string userId, int productId)
        {
            var result = await _cartManager.RemoveFromCartAsync(userId, productId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPut("{userId}/items")]
        public async Task<ActionResult<GeneralResult<CartDto>>> UpdateQuantity(string userId, [FromBody] UpdateCartItemQuantityDto dto)
        {
            var result = await _cartManager.UpdateQuantityAsync(userId, dto);

            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{userId:string}")]
        public async Task<ActionResult<GeneralResult>> ClearCart(string userId)
        {
            var result = await _cartManager.ClearCartAsync(userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
