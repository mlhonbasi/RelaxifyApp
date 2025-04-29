using Application.Services.Contents.MainContent;
using Application.Services.Contents.MainContent.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/admin/contents")]
    public class AdminContentController(IContentService contentService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllContents()
        {
            return await contentService.GetAllAsync() switch
            {
                var contents when contents != null => Ok(contents),
                _ => NotFound(new { error = "No content found" })
            };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContentById(Guid id)
        {
            return await contentService.GetByIdAsync(id) switch
            {
                var content when content != null => Ok(content),
                _ => NotFound(new { error = "Content not found" })
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContent(Guid id, [FromBody] UpdateContentRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { error = "Invalid request" });
            }
            await contentService.UpdateAsync(id, request);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(Guid id)
        {
            await contentService.DeleteAsync(id);
            return NoContent();
        }
        //BURADAN DEVAM EDECEGİM...
    }
}
