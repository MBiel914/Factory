namespace Factory.API.Service.DTOs.Extras
{
    public class PagedResultDto<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
    }
}
