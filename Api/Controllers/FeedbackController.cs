using Application.Services.ContentFeedback.Models;
using Application.Services.ContentLogs;
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
    }
}
