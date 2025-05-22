using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StressTestResultRepository(RelaxifyDbContext context) : GenericRepository<StressTestResult>(context), IStressTestResultRepository
    {
        public async Task<StressTestResult?> GetLastUserTestResult(Guid userId)
        {
            return await context.StressTestResults
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<StressTestResult>> GetUserResultsAsync(Guid userId)
        {
            return await context.StressTestResults
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }
    }
}
