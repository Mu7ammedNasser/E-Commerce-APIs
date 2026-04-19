using ECommerce.Common.Enums;

namespace ECommerce.DAL
{
    public class Order : IAuditable
    {
        // ------------------------------------------------------------- Properties
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending; // e.g., Pending, Shipped, Delivered, Cancelled
        // ------------------------------------------------------------- Relationships
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        // ------------------------------------------------------------- Auditing
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
