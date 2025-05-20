using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StressQuestionRepository(RelaxifyDbContext context) : GenericRepository<StressQuestion>(context), IStressQuestionRepository
    {
        public async Task<IList<StressQuestion>> GetAllWithAnswersAsync()
        {
            return await context.StressQuestions
                .Include(q => q.Answers.OrderBy(a => a.Order))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
