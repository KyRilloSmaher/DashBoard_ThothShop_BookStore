namespace ThothStore_DashBoard.Models.Admin.VM.NewFolder
{
    public class AdminHomeViewModel
    {
        
            public ICollection<AdminResponse> Admins { get; set; } = new HashSet<AdminResponse>();
            public int TotalAdmins { get; set; }
            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }
    }
}
