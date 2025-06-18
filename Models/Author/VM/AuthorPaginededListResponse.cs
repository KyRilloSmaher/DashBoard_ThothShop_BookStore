using ThothStore_DashBoard.Models.Book;

namespace ThothStore_DashBoard.Models.Author.VM
{
    public class AuthorPaginededListResponse
    {
        public IEnumerable<AuthorResponse>? authors { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
