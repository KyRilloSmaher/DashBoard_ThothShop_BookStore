using System.ComponentModel.DataAnnotations;

namespace ThothStore_DashBoard.Models.Admin
{
    public class ConfirmReseetPasswordCodeRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
