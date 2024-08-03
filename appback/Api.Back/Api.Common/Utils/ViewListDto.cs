namespace Api.Common.Utils
{
    public class ViewListDto<T>
    {
        public int? Count { get; set; }
        public PagedList<T> List { get; set; } = default!;
    }
}