using Application.Services.StressTestResult.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.StressTestResult
{
    public class StressTestResultService(IStressTestResultRepository stressTestResultRepository) : IStressTestResultService
    {
        public async Task SaveResultAsync(Guid userId, StressTestResultRequest request)
        {
            var result = new Domain.Entities.StressTestResult
            {
                UserId = userId,
                Score = request.Score,
                StressLevel = request.StressLevel,
                CreatedAt = DateTime.UtcNow
            };

            await stressTestResultRepository.AddAsync(result);

        }
        public async Task<IList<Domain.Entities.StressTestResult>> GetUserResultsAsync(Guid userId)
        {
            return await stressTestResultRepository.GetUserResultsAsync(userId);
        }

        public async Task<Domain.Entities.StressTestResult?> GetLastResultAsync(Guid userId)
        {
            return await stressTestResultRepository.GetLastUserTestResult(userId);
        }
    }
}
