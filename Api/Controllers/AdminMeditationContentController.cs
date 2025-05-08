using Application.Services.Contents.MeditationContents;
using Application.Services.Contents.MeditationContents.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/admin/meditationcontents")]
    public class AdminMeditationContentController(IMeditationContentService meditationContentService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateMeditationContent([FromBody] CreateMeditationContentRequest request)
        {
            if (request == null) { return BadRequest(new { error = "Invalid request" }); }
            await meditationContentService.CreateMeditationContentAsync(request);
            return NoContent();
        }

        [HttpGet("{contentId}")]
        public async Task<IActionResult> GetMeditationContentById(Guid contentId)
        {
            if (contentId == Guid.Empty) { return BadRequest(new { error = "Invalid content ID" }); }
            var breathingContent = await meditationContentService.GetByContentIdAsync(contentId);
            if (breathingContent == null) { return NotFound(new { error = "Meditation content not found" }); }
            return Ok(breathingContent);
        }

        [HttpPut("{contentId}")]
        public async Task<IActionResult> UpdateMeditationContent(Guid contentId, [FromBody] UpdateMeditationContentRequest request)
        {
            if (request == null) { return BadRequest(new { error = "Invalid request" }); }
            await meditationContentService.UpdateAsync(contentId, request);
            return NoContent();
        }

        [HttpDelete("{contentId}")]
        public async Task<IActionResult> DeleteMeditationContent(Guid contentId)
        {
            await meditationContentService.DeleteAsync(contentId);
            return NoContent();
        }



    }
}
