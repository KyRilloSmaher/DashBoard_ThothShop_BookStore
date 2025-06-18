namespace ThothStore_DashBoard.Models.Category
{
    public class UpdateCategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
