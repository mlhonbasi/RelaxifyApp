using Application.Services.AI.Models;
using Application.Services.Users;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services.AI
{
    public class StressPredictionInputBuilderService(IUserContentLogRepository logRepo, IStressTestResultRepository stressRepo, IUserService userService) : IStressPredictionInputBuilderService
    {
        public async Task<StressPredictionInputModel> BuildInputAsync()
        {
            var userId = await userService.GetUserIdAsync();
            var lastLog = await logRepo.GetLastAsync(userId);
            var lastStress = await stressRepo.GetLastAsync(userId);

            if (lastLog == null || lastStress == null)
                throw new Exception("Yetersiz geçmiş veri.");

            return new StressPredictionInputModel
            {
                CategoryEncoded = MapCategoryToInt(lastLog.Category),
                DurationInSeconds = lastLog.DurationInSeconds,
                Hour = lastLog.CreatedAt.Hour,
                DayOfWeek = (int)lastLog.CreatedAt.DayOfWeek,
                PreviousStressScore = lastStress.Score,
                SessionCountToday = 3, // isteğe bağlı olarak hesaplanabilir
                AverageDailyStress = 5.4f // örnek değeri sonra loglardan hesaplayabiliriz
            };
        }

        private int MapCategoryToInt(ContentCategory category)
        {
            return category switch
            {
                ContentCategory.Breathing => 0,
                ContentCategory.Meditation => 1,
                ContentCategory.Music => 2,
                ContentCategory.Game => 3,
                _ => 4
            };
        }
    }
}
