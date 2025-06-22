using Application.Services.AI;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController(IAiTrainingService aiService) : ControllerBase
    {
        [HttpPost("retrain")]
        public async Task<IActionResult> RetrainModel()
        {
            var result = await aiService.RetrainModelAsync();
            if (result)
                return Ok(new { success = true, message = "Model retrain işlemi başarılı." });

            return StatusCode(500, new { success = false, message = "Model retrain işlemi başarısız." });
        }
    }
}
