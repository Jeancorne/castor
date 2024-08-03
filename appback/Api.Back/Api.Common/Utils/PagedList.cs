namespace Api.Common.Utils
{
    public class PagedList<T> : List<T>
    {
        public PagedList(List<T> items, int count, int? pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalCount = count;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public int? CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int? PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;

        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}