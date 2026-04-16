namespace ECommerce.DAL
{
    public class Product : IAuditable
    {
        //------------------------------------------------------------// Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ProductsInStock { get; set; }
        public string? ImageUrl { get; set; }
        ///------------------------------------------------------------// Relationships
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        //------------------------------------------------------------// Auditing
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
