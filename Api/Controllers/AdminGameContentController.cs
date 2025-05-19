using Application.Services.Contents.BreathingContents.Models;
using Application.Services.Contents.BreathingContents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Contents.GameContent.Models;
using Application.Services.Contents.GameContent;

namespace Api.Controllers
{  
    [ApiController]
    [Route("api/[controller]")]
    public class AdminGameContentController(IGameContentService gameContentService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateGameContent([FromBody] CreateGameContentRequest request)
        {
            if (request == null) { return BadRequest(new { error = "Invalid request" }); }
            await gameContentService.CreateGameContentAsync(request);
            return NoContent();
        }

        [HttpGet("{contentId}")]
        public async Task<IActionResult> GetGameContentById(Guid contentId)
        {
            if (contentId == Guid.Empty) { return BadRequest(new { error = "Invalid content ID" }); }
            var breathingContent = await gameContentService.GetByContentIdAsync(contentId);
            if (breathingContent == null) { return NotFound(new { error = "Breathing content not found" }); }
            return Ok(breathingContent);
        }

        [HttpPut("{contentId}")]
        public async Task<IActionResult> UpdateGaeContent(Guid contentId, [FromBody] UpdateGameContentRequest request)
        {
            if (request == null) { return BadRequest(new { error = "Invalid request" }); }
            await gameContentService.UpdateAsync(contentId, request);
            return NoContent();
        }

        [HttpDelete("{contentId}")]
        public async Task<IActionResult> DeleteGameContent(Guid contentId)
        {
            await gameContentService.DeleteAsync(contentId);
            return NoContent();
        }
    }
}
