using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IStressQuestionRepository : IRepository<StressQuestion>
    {
        Task<IList<StressQuestion>> GetAllWithAnswersAsync();
    }
}
