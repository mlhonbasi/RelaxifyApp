using Application.Services.Authentications.Login.DTOs;
using Application.Services.Authentications.Login.Models;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Authentications.Login
{
    public class LoginService(IUserRepository userRepository, IConfiguration _configuration): ILoginService
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
                AccessToken = GenerateAccessToken(user),
                ExpireDate = DateTime.Now.AddHours(1),
            };
        }
        private string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                  new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(1); 

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
