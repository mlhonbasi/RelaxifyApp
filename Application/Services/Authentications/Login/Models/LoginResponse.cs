namespace Application.Services.Authentications.Login.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public DateTime ExpireDate { get; set; }
        //public string RefreshToken { get; set; } // opsiyonel
    }

}
