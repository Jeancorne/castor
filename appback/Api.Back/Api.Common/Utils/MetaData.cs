namespace Api.Common.Utils
{
    public class MetaData<T>
    {
        public MetaData(T data)
        {
            Data = data;
        }

        public int? CountTotal { get; set; }
        public int? CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int? PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public string? PreviousPageUrl { get; set; }
        public string? NextPageUrl { get; set; }
        public T Data { get; set; }
    }
}