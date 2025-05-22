using Application.Services.Contents.BreathingContents;
using Application.Services.Contents.BreathingContents.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/admin/breathingcontents")]
    public class AdminBreathingContentController(IBreathingContentService breathingContentService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateBreathingContent([FromBody] CreateBreathingContentRequest request)
        {
            if (request == null) { return BadRequest(new { error = "Invalid request" }); }
            await breathingContentService.CreateBreathingContentAsync(request);
            return NoContent();
        }

        [HttpGet("{contentId}")]
        public async Task<IActionResult> GetBreathingContentById(Guid contentId)
        {
            if (contentId == Guid.Empty) { return BadRequest(new { error = "Invalid content ID" }); }
            var breathingContent = await breathingContentService.GetByContentIdAsync(contentId);
            if (breathingContent == null) { return NotFound(new { error = "Breathing content not found" }); }
            return Ok(breathingContent);
        }

        [HttpPut("{contentId}")]
        public async Task<IActionResult> UpdateBreathingContent(Guid contentId, [FromBody] UpdateBreathingContentRequest request)
        {
            if (request == null) { return BadRequest(new { error = "Invalid request" }); }
            await breathingContentService.UpdateAsync(contentId, request);
            return NoContent();
        }

        [HttpDelete("{contentId}")]
        public async Task<IActionResult> DeleteBreathingContent(Guid contentId)
        {
            await breathingContentService.DeleteAsync(contentId);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await breathingContentService.GetAllAsync();
            return Ok(result);
        }
    }

}
