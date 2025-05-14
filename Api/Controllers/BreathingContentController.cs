using Application.Services.Contents.BreathingContents;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreathingContentController(IBreathingContentService breathingContentService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await breathingContentService.GetAllAsync();
            return Ok(result);
        }
    }
}
