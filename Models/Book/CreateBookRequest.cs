using System.ComponentModel.DataAnnotations;

namespace ThothStore_DashBoard.Models.Book
{
    public class CreateBookRequest
    {
        [Required]
        public string CategoryId { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public IFormFile PrimaryImage { get; set; } = null!;
        public List<IFormFile>? Images { get; set; }
    }
}
