namespace ECommerce.DAL
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart?> GetCartByUserIdAsync(string userId);
        Task<CartItem?> GetCartItemByProductIdAsync(int cartId, int productId);
    }
}
