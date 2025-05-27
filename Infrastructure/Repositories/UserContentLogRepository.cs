using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models.Queries;
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
        public async Task<LastPlayedContentDto?> GetLastPlayedMusicAsync(Guid userId)
        {
            return await context.UserContentLogs
                .Where(l => l.UserId == userId && l.Category == ContentCategory.Music)
                .OrderByDescending(l => l.CreatedAt)
                .Select(l => new LastPlayedContentDto
                {
                    ContentId = l.ContentId,
                    PlayedAt = l.CreatedAt,
                    Title = context.Contents
                        .Where(c => c.Id == l.ContentId)
                        .Select(c => c.Title)
                        .FirstOrDefault() ?? "(Bilinmiyor)"
                })
                .FirstOrDefaultAsync();
        }
    }
}
