using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL
{
    public class OrderItem : IAuditable
    {
        // ------------------------------------------------------------- Properties
        public int Id { get; set; }
        public int Quantity { get; set; }

        // ------------------------------------------------------------- Relationships
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        // ------------------------------------------------------------- Auditing
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }
    }
}
