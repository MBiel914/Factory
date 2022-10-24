namespace Factory.API.Service.DTOs.Extras
{
    public record PagedResultDto<T>
    {
        public int TotalCount { get; init; }
        public int PageSize { get; init; }
        public List<T> Items { get; set; }
    }
}
