namespace ECommerce
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public  int  ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductIamgeUrl { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }
}
