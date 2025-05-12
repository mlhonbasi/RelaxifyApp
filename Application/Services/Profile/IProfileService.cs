using Application.Services.Profile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Profile
{
    public interface IProfileService
    {
        public Task UpdateFullNameAsync(Guid userId, UpdateNameRequest req);
        public Task UpdatePasswordAsync(Guid userId, UpdatePasswordRequest req);
    }
}
