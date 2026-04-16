
namespace ECommerce.DAL
{
    public class CartItem : IAuditable
    {

        // ------------------------------------------------------------// Properties
        public int Id { get; set; }
        public int Quantity { get; set; }

        // ------------------------------------------------------------// Relationships
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        // ------------------------------------------------------------// Auditing
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set  ; }
    }
}