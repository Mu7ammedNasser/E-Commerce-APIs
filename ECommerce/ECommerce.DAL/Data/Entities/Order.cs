using ECommerce.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // statues should be an enum, so i will create an enum for it
        // but i did migrate what is the steps to do this i have common layer by the way 
        // 1. Create an enum for OrderStatus in the Common layer
        // 2. Update the Order class to use the enum instead of a string for Status
        // 3. Update the database schema to reflect the change (this may involve creating a new migration and updating the database)
        public OrderStatus Status { get; set; } = OrderStatus.Pending; // e.g., Pending, Shipped, Delivered, Cancelled
        // ------------------------------------------------------------- Relationships
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        // ------------------------------------------------------------- Auditing
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }
    }
}
