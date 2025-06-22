using Application.Services.Contents.MainContent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController(IContentService contentService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContentById(Guid id)
        {
            var content = await contentService.GetContentByIdAsync(id);
            if (content == null)
                return NotFound(new { message = "İçerik bulunamadı." });

            return Ok(content);
        }
    }
}
