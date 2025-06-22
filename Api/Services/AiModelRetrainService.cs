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
                    await trainingService.RetrainModelAsync();
                }

                Console.WriteLine($"[AI Retrain] Tamamlandı. Sonraki {_interval} sonra tekrar.");

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
