using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GameContentRepository(RelaxifyDbContext context) : GenericRepository<GameContent>(context), IGameContentRepository
    {
        public async Task<IList<GameContent>> GetWithContentAsync()
        {
            return await context.GameContents
                .Include(b => b.Content)
                .Where(b => b.Content.IsActive)
                .ToListAsync();
        }
        public async Task<GameContent?> GetWithContentByIdAsync(Guid id)
        {
            return await context.GameContents
               .Include(b => b.Content)
               .Where(b => b.ContentId == id)
               .AsNoTracking()
               .FirstOrDefaultAsync();
        }
    }
}
