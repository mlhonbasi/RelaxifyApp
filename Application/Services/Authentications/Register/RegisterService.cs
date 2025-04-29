using Application.Services.Authentications.Register.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Authentications.Register
{
    public class RegisterService(IUserRepository userRepository) : IRegisterService
    {
        public async Task RegisterAsync(RegisterRequest request)
        {
            if(request.Password != request.ConfirmPassword){
                throw new Exception("Şifreler eşleşmiyor!");
            }

            try
            {
                var user = new User
                {
                    Email = request.Email,
                    Name = request.Name,
                    Surname = request.Surname,
                    Password = HashPassword(request.Password)
                };
              await userRepository.AddAsync(user);
            }
            catch
            {
                throw new Exception("Kullanıcı eklemede hata!");
            }

        }
        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
