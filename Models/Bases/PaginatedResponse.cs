namespace ThothStore_DashBoard.Models.Bases
{
    public class PaginatedResponse<T>
    {
        #region Fields
        public IEnumerable<T> Data { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public object Meta { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public List<string> Messages { get; set; } = new();

        public bool Succeeded { get; set; }
        #endregion
    }
}
