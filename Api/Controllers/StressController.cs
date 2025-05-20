using Application.Services.Stress;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StressController(IStressService stressService) : ControllerBase
    {
        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await stressService.GetAllQuestionsAsync();

            return Ok(questions); // DTO kullanılacaksa sonra dönüştürürüz
        }
    }
}
