using Application.Services.ContentFeedback.Models;
using Application.Services.ContentLogs;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController(IContentFeedbackService feedbackService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackRequest request)
        {
            if (request.ContentId == Guid.Empty || request.Duration <= 0)
            {
                return BadRequest(new { error = "Geçersiz içerik veya süre" });
            }

            await feedbackService.CreateAsync(request);
            return Ok(new { success = true, message = "Geri bildirim kaydedildi." });
        }

        [HttpGet("music-summary")]
        public async Task<IActionResult> GetMusicSummary([FromQuery] SummaryRange range = SummaryRange.Week)
        {
            var result = await feedbackService.GetMusicSummaryAsync(range);
            return Ok(result);
        }
    }
}
    
