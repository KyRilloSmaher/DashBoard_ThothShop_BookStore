using ThothStore_DashBoard.Models.Book;

namespace ThothStore_DashBoard.Models.Order
{
    public class OrderPaginetedListResponse
    {
        public IEnumerable<OrderResponse>? orders { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
