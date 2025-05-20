using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IStressTestResultRepository : IRepository<StressTestResult>
    {
        Task<IList<StressTestResult>> GetUserResultsAsync(Guid userId);
    }
}
