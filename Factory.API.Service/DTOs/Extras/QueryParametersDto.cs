namespace Factory.API.Service.DTOs.Extras
{
    public class QueryParametersDto
    {
        private int _pageSize = 15;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public int StartIndex { get; set; }
    }
}
