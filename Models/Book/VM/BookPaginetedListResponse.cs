namespace ThothStore_DashBoard.Models.Book.VM
{
    public class BookPaginetedListResponse
    {
        public IEnumerable<BookResponse>? books { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<BookResponse>? PopularBooks { get; set; }
        public IEnumerable<BookResponse>? OutofStockBooks { get; set; }
    }
}
