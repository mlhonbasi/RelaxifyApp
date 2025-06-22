using Application.Services.AI.Models;
using System.Net.Http.Json;

namespace Application.Services.AI
{
    public class StressPredictionService(HttpClient httpClient) : IStressPredictionService
    {
        public async Task<float?> PredictStressAsync(StressPredictionInputModel input)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("http://localhost:5000/predict-stress", input);
                if (!response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Flask stres tahmini başarısız: {msg}");
                    return null;
                }

                var result = await response.Content.ReadFromJsonAsync<StressPredictionResult>();
                return result?.PredictedStressScore;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StressPrediction] Hata: {ex.Message}");
                return null;
            }
        }

        private class StressPredictionResult
        {
            public float PredictedStressScore { get; set; }
        }
    }
}
