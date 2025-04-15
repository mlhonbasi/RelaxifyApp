using Application.Services.Authentications.Login.DTOs;
using Application.Services.Authentications.Login.Models;

namespace Application.Services.Authentications.Login
{
    public interface ILoginService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
