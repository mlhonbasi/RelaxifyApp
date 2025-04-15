using Application.Services.Authentications.Login.DTOs;
using Application.Services.Authentications.Login.Models;
using Domain.Interfaces;

namespace Application.Services.Authentications.Login
{
    public class LoginService(IUserRepository userRepository): ILoginService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await userRepository.GetUserEmailAsync(request.Email);
            if(user == null)
            {
                throw new Exception("Kullanıcı bulunamadı!");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new Exception("Şifre hatalı!");
            }
            return new LoginResponse
            {
                Token = "asd",
                ExpiresAt = DateTime.UtcNow.AddHours(1),
            };
        }
    }
}
