using Application.Services.Users;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController(IUserContentLogRepository logRepo, IStressTestResultRepository stressRepo, IContentRepository contentRepo , IUserService userService) : ControllerBase
    {
        [HttpGet("recommendation-input")]
        public async Task<IActionResult> GetRecommendationInput()
        {
            var userId = await userService.GetUserIdAsync();
            if (userId == Guid.Empty)
                return Unauthorized();

            var lastLog = await logRepo.GetLastAsync(userId);
            var lastStress = await stressRepo.GetLastAsync(userId);

            if (lastLog == null || lastStress == null)
                return NotFound("Yeterli geçmiş veri yok.");

            var input = new
            {
                CategoryEncoded = MapCategoryToInt(lastLog.Category),
                DurationInSeconds = lastLog.DurationInSeconds,
                hour = lastLog.CreatedAt.Hour,
                dayofweek = (int)lastLog.CreatedAt.DayOfWeek,
                LastStressScore = lastStress.Score
            };

            using var client = new HttpClient();
            Console.WriteLine("Flask'a gönderilen veri:");
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(input));
            var flaskResponse = await client.PostAsJsonAsync("http://127.0.0.1:5000/predict", input);

            if (!flaskResponse.IsSuccessStatusCode)
            {
                var errorText = await flaskResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"[Flask ERROR Response]: {errorText}");
                return StatusCode(500, "Flask servisi başarısız.");
            }

            var result = await flaskResponse.Content.ReadFromJsonAsync<FlaskResponse>();

            var content = await contentRepo.GetByIdAsync(Guid.Parse(result.recommendedContentId));
            if (content == null)
                return NotFound("Önerilen içerik bulunamadı.");

            return Ok(new
            {
                content.Id,
                content.Title,
                content.Description,
                content.ImagePath,
                content.Category
            });
        }
        private class FlaskResponse
        {
            public string recommendedContentId { get; set; } = null!;
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
