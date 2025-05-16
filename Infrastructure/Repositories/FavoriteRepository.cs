using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository(RelaxifyDbContext context) : GenericRepository<UserFavorite>(context), IFavoriteRepository
    {
        public async Task<UserFavorite> GetFavoriteByUserAndContentId(Guid userId, Guid contentId)
        {
            return await context.UserFavorites.FirstOrDefaultAsync(f => f.UserId == userId && f.ContentId == contentId);
        }

        public async Task<List<Guid>> GetFavoritesByUserIdAsync(Guid userId)
        {
            return await context.UserFavorites
                .Where(f => f.UserId == userId)
                .Select(f => f.ContentId)
                .ToListAsync();
        }
    }
}
