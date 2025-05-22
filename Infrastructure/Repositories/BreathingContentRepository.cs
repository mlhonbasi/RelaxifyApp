using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BreathingContentRepository(RelaxifyDbContext context) :GenericRepository<BreathingContent>(context), IBreathingContentRepository
    {
        public async Task<IList<BreathingContent>> GetWithContentAsync()
        {
            return await context.BreathingContents
                .Include(b => b.Content)
                .Where(b => b.Content.IsActive)
                .ToListAsync();
        }
        public async Task<BreathingContent?> GetWithContentByIdAsync(Guid id)
        {
            return await context.BreathingContents
                .Include(b => b.Content)
                .Where(b => b.ContentId == id)
                .FirstOrDefaultAsync();
        }
    }
}
