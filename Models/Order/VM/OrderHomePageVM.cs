namespace ThothStore_DashBoard.Models.Order.VM
{
    public class OrderHomePageVM
    {
        public int? orderCount { get; set; }
        public decimal? TotalSales { get; set; }
        public OrderPaginetedListResponse? Allorders { get; set; }
        public IEnumerable<OrderResponse>? RecentOrders { get; set; }
        public IEnumerable<OrderResponse>? UnCompleteOrders { get; set; }
    }
}
