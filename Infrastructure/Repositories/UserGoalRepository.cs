using Domain.Entities;
using Domain.Enums;
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
    public class UserGoalRepository(RelaxifyDbContext context) : GenericRepository<UserGoal>(context), IUserGoalRepository
    {
        public async Task<List<UserGoal>> GetGoalsForUserAsync(Guid userId)
        {
            return await context.UserGoals
                .Where(g => g.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserGoal?> GetGoalByCategoryAsync(Guid userId, ContentCategory category)
        {
            return await context.UserGoals
                .FirstOrDefaultAsync(g => g.UserId == userId && g.Category == category);
        }
    }
}
