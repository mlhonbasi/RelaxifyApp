using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MeditationContentRepository(RelaxifyDbContext context) :GenericRepository<MeditationContent>(context), IMeditationContentRepository
    {
        public async Task<IList<MeditationContent>> GetWithContentAsync()
        {
            return await context.MeditationContents
                .Include(b => b.Content)
                .Where(b => b.Content.IsActive)
                .ToListAsync();
        }
        public async Task<MeditationContent?> GetWithContentByIdAsync(Guid id)
        {
            return await context.MeditationContents
               .Include(b => b.Content)
               .Where(b => b.ContentId == id)
               .FirstOrDefaultAsync();
        }
    }
}
