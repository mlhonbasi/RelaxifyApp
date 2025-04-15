using Application.Services.Authentications.Register.Models;

namespace Application.Services.Authentications.Register
{
    public interface IRegisterService
    {
        Task RegisterAsync(RegisterRequest request);
    }
}
