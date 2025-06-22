using Application.Services.AI;
using Application.Services.AI.Models;
using Application.Services.Stress;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StressController(IStressService stressService, IStressPredictionService stressPredictionService, IStressPredictionInputBuilderService stressInputBuilderService) : ControllerBase
    {
        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await stressService.GetAllQuestionsAsync();

            return Ok(questions); // DTO kullanılacaksa sonra dönüştürürüz
        }
        [HttpGet("predict")]
        public async Task<IActionResult> PredictStress()
        {
            var input = await stressInputBuilderService.BuildInputAsync();
            var prediction = await stressPredictionService.PredictStressAsync(input);
            if (prediction == null)
                return StatusCode(500, "Tahmin alınamadı");

            return Ok(new { predictedStress = prediction });
        }
    }
}
