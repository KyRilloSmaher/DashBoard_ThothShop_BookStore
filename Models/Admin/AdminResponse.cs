namespace ThothStore_DashBoard.Models.Admin
{
    public class AdminResponse
    {
        public string Id { set; get; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
