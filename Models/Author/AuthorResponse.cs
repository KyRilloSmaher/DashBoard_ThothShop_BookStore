using System.Reflection;
using ThothStore_DashBoard.Models.Enums;

namespace ThothStore_DashBoard.Models.Author
{
    public class AuthorResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public Gender Gender { get; set; }
        public Nationality Nationality { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
