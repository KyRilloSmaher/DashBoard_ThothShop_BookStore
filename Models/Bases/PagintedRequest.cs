namespace ThothStore_DashBoard.Models.Bases
{
    public class PagintedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
