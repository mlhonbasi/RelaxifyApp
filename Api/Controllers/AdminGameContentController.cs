using Application.Services.Contents.BreathingContents.Models;
using Application.Services.Contents.BreathingContents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Contents.GameContent.Models;
using Application.Services.Contents.GameContent;

namespace Api.Controllers
{  
    [ApiController]
    [Route("api/admin/gamecontents")]
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
            var gameContent = await gameContentService.GetByContentIdAsync(contentId);
            if (gameContent == null) { return NotFound(new { error = "Breathing content not found" }); }
            return Ok(gameContent);
        }

        [HttpPut("{contentId}")]
        public async Task<IActionResult> UpdateGameContent(Guid contentId, [FromBody] UpdateGameContentRequest request)
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await gameContentService.GetAllAsync();
            return Ok(result);
        }
    }
}
