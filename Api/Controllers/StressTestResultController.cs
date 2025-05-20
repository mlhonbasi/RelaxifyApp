using Application.Services.StressTestResult;
using Application.Services.StressTestResult.Models;
using Application.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class StressTestResultController(IStressTestResultService stressTestResultService, IUserService userService) : ControllerBase
    {
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitResult([FromBody] StressTestResultRequest request)
        {
            var userId = await userService.GetUserIdAsync();

            await stressTestResultService.SaveResultAsync(userId, request);

            return Ok(new
            {
                success = true,
                message = "Test sonucu başarıyla kaydedildi."
            });
        }
        [HttpGet("user-results")]
        public async Task<IActionResult> GetUserResults()
        {
            var userId = await userService.GetUserIdAsync();
            var results = await stressTestResultService.GetUserResultsAsync(userId);
            return Ok(results);
        }


    }
}
