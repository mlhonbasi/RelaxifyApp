using Application.Services.AI;

namespace Api.Services
{  
    public class AiModelRetrainService(IServiceScopeFactory scopeFactory) : BackgroundService
    {
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"[AI Retrain] Başlıyor...");

                using (var scope = scopeFactory.CreateScope())
                {
                    var trainingService = scope.ServiceProvider.GetRequiredService<IAiTrainingService>();
                    var recommendationSuccess = await trainingService.RetrainModelAsync();
                    var stressSuccess = await trainingService.RetrainStressModelAsync();
                    Console.WriteLine($" Öneri retrain: {(recommendationSuccess ? "Olumlu" : "Olumsuz")}");
                    Console.WriteLine($" Stres retrain: {(stressSuccess ? "Olumlu" : "Olumsuz")}");
                }

                Console.WriteLine($"[AI Retrain] Tamamlandı. Sonraki {_interval} sonra tekrar.");

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
