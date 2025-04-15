namespace Application.Services.Authentications.Login.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        //public string RefreshToken { get; set; } // opsiyonel
        public DateTime ExpiresAt { get; set; }
    }

}
