using System.ComponentModel.DataAnnotations;

namespace ThothStore_DashBoard.Models.Admin
{
    public class UpdateAdminModel
    {
        public Guid Id { set; get; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        public string? Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Phone number must be 11 Number .")]
        public string? PhoneNumber { get; set; }
    }
}
