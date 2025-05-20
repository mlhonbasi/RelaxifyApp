using Application.Services.StressTestResult.Models;

namespace Application.Services.StressTestResult
{
    public interface IStressTestResultService
    {
        Task SaveResultAsync(Guid userId, StressTestResultRequest request);
        Task<IList<Domain.Entities.StressTestResult>> GetUserResultsAsync(Guid userId);

    }
}
