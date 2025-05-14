using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Users
{
    public class UserService(IUserRepository userRepository, IFavoriteRepository favoriteRepository, IHttpContextAccessor httpContextAccessor) : IUserService
    {
        public async Task<Guid> GetUserIdAsync()
        {
            var userIdString = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdString, out var userId))
            {
                return userId;
            }

            throw new Exception("Geçersiz veya eksik kullanıcı ID.");
        }

        public async Task<IList<Guid>> GetUserFavorites()
        {
            var userId = await GetUserIdAsync();

            return await favoriteRepository.GetFavoritesByUserIdAsync(userId);
        }
    }
}
