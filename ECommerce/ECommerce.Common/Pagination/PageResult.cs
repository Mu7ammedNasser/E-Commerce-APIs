namespace ECommerce.Common
{
    public class PageResult<T>
    {
        public IReadOnlyList<T> Items { get; set; }
        public PaginationMetadata Metadata { get; set; } = new();

    }
}
