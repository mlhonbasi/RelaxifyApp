using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserContentLogRepository(RelaxifyDbContext context) : GenericRepository<UserContentLog>(context), IUserContentLogRepository
    {
        public async Task<List<UserContentLog>> GetCategoryUsageAsync(Guid userId)
        {
            return await context.UserContentLogs
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
        public async Task<List<UserContentLog>> GetLogsByUserIdAsync(Guid userId)
        {
            return await context.UserContentLogs
                .Where(log => log.UserId == userId)
                .OrderByDescending(log => log.CreatedAt)
                .ToListAsync();
        }
    }
}
