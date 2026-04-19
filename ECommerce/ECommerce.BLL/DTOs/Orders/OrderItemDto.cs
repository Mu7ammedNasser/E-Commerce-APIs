namespace ECommerce.BLL
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductId { get; set; }
        public int ProductName { get; set; }
        public decimal LineTotal { get; set; }
    }
}
