using Application.Services.Contents.MainContent.Models;
using Application.Services.Contents.MainContent;
using Application.Services.Contents.MusicContents;
using Application.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class MusicContentController(IMusicContentService musicContentService, IContentService contentService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await musicContentService.GetAllAsync();
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
