using Application.Services.Contents.GameContent;
using Application.Services.Contents.MainContent;
using Application.Services.Contents.MainContent.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameContentController(IContentService contentService, IGameContentService gameContentService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await gameContentService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await gameContentService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

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
