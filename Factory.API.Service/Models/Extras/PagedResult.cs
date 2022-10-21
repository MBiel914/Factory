namespace Factory.API.Service.Models.Extras
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
    }
}
