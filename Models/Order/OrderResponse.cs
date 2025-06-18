using ThothStore_DashBoard.Models.Enums;

namespace ThothStore_DashBoard.Models.Order
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<OrderItemResponse> Items { get; set; } = new HashSet<OrderItemResponse>();
    }
}
