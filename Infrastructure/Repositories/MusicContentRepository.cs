using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MusicContentRepository(RelaxifyDbContext context) : GenericRepository<MusicContent>(context),IMusicContentRepository
    {
        public async Task<IList<MusicContent>> GetWithContentAsync()
        {
            return await context.MusicContents
                .Include(b => b.Content)
                .Where(b => b.Content.IsActive)
                .ToListAsync();
        }
    }
}
