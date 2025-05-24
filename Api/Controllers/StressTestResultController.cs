using Application.DTOs;
using Application.Services.StressTestResult;
using Application.Services.StressTestResult.Models;
using Application.Services.Users;
using Domain.Entities;
using Infrastructure.Repositories;
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

        [HttpGet("last")]
        public async Task<IActionResult> GetLastResult()
        {
            var userId = await userService.GetUserIdAsync();
            var result = await stressTestResultService.GetLastResultAsync(userId);
            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<StressSummaryDto>> GetSummary([FromQuery] string filter = "weekly")
        {
            var userId = await userService.GetUserIdAsync();
            var allResults = await stressTestResultService.GetUserResultsAsync(userId);

            var now = DateTime.UtcNow;
            IEnumerable<StressTestResult> filteredResults = filter.ToLower() switch
            {
                "daily" => allResults.Where(r => r.CreatedAt.Date == now.Date),
                "weekly" => allResults.Where(r => r.CreatedAt >= now.AddDays(-7)),
                "monthly" => allResults.Where(r => r.CreatedAt >= now.AddMonths(-1)),
                _ => allResults
            };

            if (!filteredResults.Any())
                return Ok(new StressSummaryDto
                {
                    Filter = filter,
                    AverageScore = 0,
                    MaxScore = 0,
                    MinScore = 0
                });

            var dto = new StressSummaryDto
            {
                Filter = filter,
                AverageScore = filteredResults.Average(r => r.Score),
                MaxScore = filteredResults.Max(r => r.Score),
                MinScore = filteredResults.Min(r => r.Score)
            };

            return Ok(dto);
        }


    }
}
