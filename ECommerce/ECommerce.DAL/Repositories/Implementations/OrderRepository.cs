using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ECommerceDbContext context) : base(context) { }
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                                 .AsNoTracking()
                                 .Include(o => o.OrderItems)
                                 .Where(o => o.UserId == userId)
                                 .OrderByDescending(o => o.OrderDate)
                                 .ToListAsync();
        }

        public async Task<Order?> GetOrderDetailsAsync(int orderId)
        {
            return await _context.Orders
                                .Include(o => o.OrderItems)
                                .ThenInclude(oi => oi.Product)
                                .Where(o => o.Id == orderId)
                                .FirstOrDefaultAsync();
        }
    }
}
