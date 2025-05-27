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
        public async Task<ContentFeedbackSummaryDto> GetMusicFeedbackSummaryAsync(Guid userId, SummaryRange range)
        {
            DateTime start = range switch
            {
                SummaryRange.Today => DateTime.UtcNow.Date,
                SummaryRange.Week => DateTime.UtcNow.Date.AddDays(-7),
                SummaryRange.Month => DateTime.UtcNow.Date.AddMonths(-1),
                _ => DateTime.MinValue
            };

            var musicLogs = await context.UserContentLogs
                .Where(f => f.UserId == userId && f.Category == ContentCategory.Music && f.CreatedAt >= start)
                .ToListAsync();

            var totalDuration = musicLogs.Sum(f => f.DurationInSeconds);

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
                    Title = "", // frontend'te eşleşir
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
                TodayDurationInMinutes = totalDuration / 60,
                MostPlayed = mostPlayedDto,
                LastPlayed = lastPlayed
            };
        }

        public async Task<MusicFeedbackDistributionDto> GetMusicFeedbackDistributionAsync(Guid userId)
        {
            var logs = await context.ContentFeedbackLogs
                .Where(f => f.UserId == userId && f.Category == ContentCategory.Music)
                .ToListAsync();

            return new MusicFeedbackDistributionDto
            {
                RelaxedCount = logs.Count(f => f.Feedback == FeedbackType.Relaxed),
                NeutralCount = logs.Count(f => f.Feedback == FeedbackType.Neutral),
                StressedCount = logs.Count(f => f.Feedback == FeedbackType.Stressed)
            };
        }

    }
}
