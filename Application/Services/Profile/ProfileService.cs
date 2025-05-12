using Application.Services.Profile.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Profile
{
    public class ProfileService(IUserRepository userRepository) : IProfileService
    {
        public async Task UpdateFullNameAsync(Guid userId, UpdateNameRequest request)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            user.Name = request.Name;
            user.Surname = request.Surname;

            await userRepository.UpdateAsync(user);
        }

        public async Task UpdatePasswordAsync(Guid userId, UpdatePasswordRequest request)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            user.Password = HashPassword(request.Password);
            await userRepository.UpdateAsync(user);
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
