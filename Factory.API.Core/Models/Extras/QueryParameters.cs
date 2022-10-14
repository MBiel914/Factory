namespace Factory.API.Core.Models.Extras
{
    public class QueryParameters
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
