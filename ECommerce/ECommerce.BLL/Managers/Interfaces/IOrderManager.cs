using ECommerce.Common;

namespace ECommerce.BLL
{
    public interface IOrderManager
    {
        Task<GeneralResult<OrderDto>> CreateOrderFromCartAsync(CreateOrderDto dto);
        Task<GeneralResult<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(string userId);
        Task<GeneralResult<OrderDto>> GetOrderByIdAsync(int orderId);
    }
}
