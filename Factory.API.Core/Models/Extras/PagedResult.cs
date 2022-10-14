namespace Factory.API.Core.Models.Extras
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
    }
}
