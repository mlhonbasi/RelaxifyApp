using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserAchievementRepository(RelaxifyDbContext context) : GenericRepository<UserAchievement>(context), IUserAchievementRepository
    {
        public async Task<List<UserAchievement>> GetByUserIdAsync(Guid userId)
        {
            return await context.UserAchievements
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
        public async Task<bool> ExistsAsync(Guid userId, string key)
        {
            return await context.UserAchievements
                .AnyAsync(x => x.UserId == userId && x.Key == key);
        }

        public async Task<List<UserAchievement>> GetUnseenAchievements(Guid userId)
        {
            return await context.UserAchievements
            .Where(x => x.UserId == userId && !x.IsSeenByUser)
            .ToListAsync();
        }
    }
}
