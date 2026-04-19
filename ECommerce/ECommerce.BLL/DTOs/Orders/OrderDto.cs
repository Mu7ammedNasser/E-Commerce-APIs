using ECommerce.Common.Enums;

namespace ECommerce.BLL
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new();

    }
}
