namespace ECommerce.Common
{
    public class PagedFilterParameters : PaginationParameters
    {
        public string? Search { get; set; }
        public string? SortedBy { get; set; }
        public bool? SortedDescending { get; set; }
    }
}