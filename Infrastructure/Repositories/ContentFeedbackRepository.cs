using Domain.Entities;
using Domain.Interfaces;
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
    }
}
