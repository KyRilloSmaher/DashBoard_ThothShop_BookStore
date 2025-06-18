namespace ThothStore_DashBoard.Models.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public RefreshTokenResponse RefreshToken { get; set; }
    }

    public class RefreshTokenResponse
    {
        public string UserName { get; set; }
        public string TokenString { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}