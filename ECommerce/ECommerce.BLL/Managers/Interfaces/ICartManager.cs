using ECommerce.Common;

namespace ECommerce.BLL
{
    public interface ICartManager
    {
        Task<GeneralResult<CartDto>> GetCartByUserIdAsync(string userId);
        Task<GeneralResult<CartDto>> AddToCartAsync(string userId, AddToCartDto addToCartDto);
        Task<GeneralResult<CartDto>> UpdateQuantityAsync(string userId, UpdateCartItemQuantityDto quantityDto);
        Task<GeneralResult> RemoveFromCartAsync(string userId, int productId);
        Task<GeneralResult> ClearCartAsync(string userId);

    }
}
