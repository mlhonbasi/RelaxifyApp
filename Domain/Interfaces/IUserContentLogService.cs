using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserContentLogService
    {
        Task LogUsageAsync(Guid userId, Guid contentId, ContentCategory category, int durationInSeconds);
    }

}
