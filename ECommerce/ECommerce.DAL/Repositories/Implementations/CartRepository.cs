
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Repositories.Implementations
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ECommerceDbContext context) : base(context) { }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts
                                 .Include(c => c.CartItems)
                                 .ThenInclude(ci => ci.Product)
                                 .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public Task<CartItem?> GetCartItemByProductIdAsync(int cartId, int productId)
        {
            return _context.CartItems
                           .Include(ci => ci.Product)
                           .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        }
    }
}
