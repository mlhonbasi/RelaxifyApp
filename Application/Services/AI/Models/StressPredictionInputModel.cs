using System.Text.Json.Serialization;

namespace Application.Services.AI.Models
{
    public class StressPredictionInputModel
    {
        [JsonPropertyName("categoryEncoded")]
        public int CategoryEncoded { get; set; }

        [JsonPropertyName("durationInSeconds")]
        public int DurationInSeconds { get; set; }

        [JsonPropertyName("hour")]
        public int Hour { get; set; }

        [JsonPropertyName("dayofweek")]
        public int DayOfWeek { get; set; }

        [JsonPropertyName("previousStressScore")]
        public float PreviousStressScore { get; set; }

        [JsonPropertyName("sessionCountToday")]
        public int SessionCountToday { get; set; }

        [JsonPropertyName("averageDailyStress")]
        public float AverageDailyStress { get; set; }
    }
}
