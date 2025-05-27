using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models.Queries;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ContentFeedbackRepository(RelaxifyDbContext context) : GenericRepository<ContentFeedbackLog>(context), IContentFeedbackRepository
    {
        public async Task<bool> HasUserGivenFeedbackAsync(Guid userId, Guid contentId)
        {
            return await context.ContentFeedbackLogs
                .AnyAsync(f => f.UserId == userId && f.ContentId == contentId);
        }
        public async Task<ContentFeedbackSummaryDto> GetMusicFeedbackSummaryAsync(Guid userId)
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            var musicLogs = await context.UserContentLogs
                .Where(f => f.UserId == userId && f.Category == ContentCategory.Music)
                .ToListAsync();

            var todayDuration = musicLogs
                .Where(f => f.CreatedAt >= today && f.CreatedAt < tomorrow)
                .Sum(f => f.DurationInSeconds);

            var mostPlayed = musicLogs
                .GroupBy(f => f.ContentId)
                .Select(g => new { g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .FirstOrDefault();

            MostPlayedContentDto? mostPlayedDto = null;

            var lastPlayed = musicLogs
                .OrderByDescending(f => f.CreatedAt)
                .Select(f => new LastPlayedContentDto
                {
                    ContentId = f.ContentId,
                    Title = "", // frontend tamamlar
                    PlayedAt = f.CreatedAt
                })
                .FirstOrDefault();
            if (mostPlayed != null)
            {
                var content = await context.Contents
                    .Where(c => c.Id == mostPlayed.Key)
                    .Select(c => new { c.Id, c.Title })
                    .FirstOrDefaultAsync();

                if (content != null)
                {
                    mostPlayedDto = new MostPlayedContentDto
                    {
                        ContentId = content.Id,
                        Title = content.Title,
                        PlayCount = mostPlayed.Count
                    };
                }
            }
            return new ContentFeedbackSummaryDto
            {
                TodayDurationInMinutes = todayDuration / 60,
                MostPlayed = mostPlayedDto,
                LastPlayed = lastPlayed
            };
        }
    }
}
