using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL
{
    public class Cart : IAuditable
    {
        //-------------------------------------------------------------- properties 
        public int Id { get; set; }

        // ------------------------------------------------------------- relationships
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();

        // ------------------------------------------------------------- auditing
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set; }
    }
}
