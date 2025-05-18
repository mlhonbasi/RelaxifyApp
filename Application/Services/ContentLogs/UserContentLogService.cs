using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ContentLogs
{
    public class UserContentLogService(IUserContentLogRepository userContentLogRepository) : IUserContentLogService
    {
        public async Task LogUsageAsync(Guid userId, Guid contentId, ContentCategory category, int durationInSeconds)
        {
            var log = new UserContentLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ContentId = contentId,
                Category = category,
                DurationInSeconds = durationInSeconds,
                CreatedAt = DateTime.UtcNow
            };
            await userContentLogRepository.AddAsync(log);
        }
    }
}
