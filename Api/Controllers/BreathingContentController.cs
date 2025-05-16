using Application.Services.Contents.BreathingContents;
using Application.Services.Contents.MainContent;
using Application.Services.Contents.MainContent.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreathingContentController(IBreathingContentService breathingContentService, IContentService contentService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await breathingContentService.GetAllAsync();
            return Ok(result);
        }
        [HttpPost("toggle-favorite")]
        public async Task<IActionResult> ToggleFavorite([FromBody] ToggleFavoriteRequest request)
        {
            var result = await contentService.ToggleFavoriteAsync(request.ContentId);

            if (!result.IsSuccess)
                return BadRequest(new { success = false, message = "Favori durumu güncellenemedi." });

            return Ok(new
            {
                success = true,
                isFavorite = result.IsFavorite
            });
        }
    }
}
