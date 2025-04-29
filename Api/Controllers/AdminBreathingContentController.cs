using Application.Services.Contents.BreathingContents;
using Application.Services.Contents.BreathingContents.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
    }
}
