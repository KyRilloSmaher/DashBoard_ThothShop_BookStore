using System.ComponentModel.DataAnnotations;
using ThothStore_DashBoard.Models.Enums;

namespace ThothStore_DashBoard.Models.Author
{
    public class CreateAuthorRequest
    {
        [Required(ErrorMessage = "Name is required")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Bio is required")]
        public string Bio { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Nationality is required")]
        public Nationality Nationality { get; set; }
        [Required(ErrorMessage = "DateOfBirth is required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
  
    }
}
