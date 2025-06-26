namespace JPBillJobDetail.Models
{
    public class PagedListModel<T, F>
    {
        public PaginationResult<T, F> Data { get; set; } = new();
    }

    public class PaginationResult<TData, TFilter>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public IEnumerable<TData> Items { get; set; } = [];
        public TFilter Filter { get; set; } = default!;

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}