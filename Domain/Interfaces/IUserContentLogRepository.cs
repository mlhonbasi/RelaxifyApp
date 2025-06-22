using Domain.Entities;
using Domain.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserContentLogRepository : IRepository<UserContentLog>
    {
        Task<List<UserContentLog>> GetCategoryUsageAsync(Guid userId);
        Task<List<UserContentLog>> GetLogsByUserIdAsync(Guid userId);
        Task<LastPlayedContentDto?> GetLastPlayedMusicAsync(Guid userId);
        Task<UserContentLog?> GetLastAsync(Guid userId);

    }
}
