namespace Factory.API.Service.DTOs.Extras
{
    public record QueryParametersDto
    {
        private int _pageSize = 15;

        public int PageSize
        {
            get { return _pageSize; }
            init { _pageSize = value; }
        }

        public int StartIndex { get; init; }
    }
}
