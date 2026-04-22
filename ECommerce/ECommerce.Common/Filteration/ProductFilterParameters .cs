namespace ECommerce.Common
{
    public class ProductFilterParameters : PagedFilterParameters
    {
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? InStockOnly { get; set; }
    }
}
