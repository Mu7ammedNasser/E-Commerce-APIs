namespace ECommerce.DAL
{
    public class Category : IAuditable
    {
        //------------------------------------------------------------// Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        //------------------------------------------------------------// Relationships
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
        //------------------------------------------------------------// Auditing
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
