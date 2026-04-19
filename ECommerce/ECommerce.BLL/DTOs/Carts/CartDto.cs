namespace ECommerce.BLL
{
    public class CartDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
