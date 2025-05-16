using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFavoriteRepository :IRepository<UserFavorite>
    {
        Task<List<Guid>> GetFavoritesByUserIdAsync(Guid userId);
        Task<UserFavorite> GetFavoriteByUserAndContentId(Guid userId, Guid contentId);
    }
}
