using Application.Services.Contents.MusicContents;
using Application.Services.Contents.MusicContents.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/admin/musiccontents")]
    public class AdminMusicContentController(IMusicContentService musicContentService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateMusicContent([FromBody] CreateMusicContentRequest request)
        {
            if (request == null) { return BadRequest(new { error = "Invalid request" }); }
            await musicContentService.CreateMusicContentAsync(request);
            return NoContent();
        }

        [HttpGet("{contentId}")]
        public async Task<IActionResult> GetMusicContentById(Guid contentId)
        {
            if (contentId == Guid.Empty) { return BadRequest(new { error = "Invalid content ID" }); }
            var musicContent = await musicContentService.GetByContentIdAsync(contentId);
            if (musicContent == null) { return NotFound(new { error = "Music content not found" }); }
            return Ok(musicContent);
        }

        [HttpPut("{contentId}")]
        public async Task<IActionResult> UpdateMusicContent(Guid contentId, [FromBody] UpdateMusicContentRequest request)
        {
            if (request == null) { return BadRequest(new { error = "Invalid request" }); }
            await musicContentService.UpdateAsync(contentId, request);
            return NoContent();
        }

        [HttpDelete("{contentId}")]
        public async Task<IActionResult> DeleteMeditationContent(Guid contentId)
        {
            await musicContentService.DeleteAsync(contentId);
            return NoContent();
        }

    }
}
