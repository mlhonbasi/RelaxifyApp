using Application.DTOs;
using Application.Services.StressTestResult;
using Application.Services.StressTestResult.Models;
using Application.Services.Users;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        public async Task<IActionResult> GetUserResults([FromQuery] string filter = "weekly")
        {
            var userId = await userService.GetUserIdAsync();
            var allResults = await stressTestResultService.GetUserResultsAsync(userId);

            var trTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime nowTr = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, trTimeZone);

            IEnumerable<StressTestResult> filteredResults = filter.ToLower() switch
            {
                "daily" => allResults.Where(r =>
                {
                    var createdTr = TimeZoneInfo.ConvertTimeFromUtc(r.CreatedAt, trTimeZone);
                    return createdTr >= nowTr.Date && createdTr < nowTr.Date.AddDays(1);
                }),

                "weekly" => allResults.Where(r =>
                {
                    var createdTr = TimeZoneInfo.ConvertTimeFromUtc(r.CreatedAt, trTimeZone);
                    return createdTr >= nowTr.Date.AddDays(-7);
                }),

                "monthly" => allResults.Where(r =>
                {
                    var createdTr = TimeZoneInfo.ConvertTimeFromUtc(r.CreatedAt, trTimeZone);
                    return createdTr >= nowTr.Date.AddMonths(-1);
                }),

                _ => allResults
            };

            return Ok(filteredResults.OrderByDescending(r => r.CreatedAt).ToList());
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

            var trTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, trTimeZone);

            IEnumerable<StressTestResult> filteredResults = filter.ToLower() switch
            {
                "daily" => allResults.Where(r => r.CreatedAt >= now.Date && r.CreatedAt < now.Date.AddDays(1)),
                "weekly" => allResults.Where(r => r.CreatedAt >= now.AddDays(-7)),
                "monthly" => allResults.Where(r => r.CreatedAt >= now.AddMonths(-1)),
                _ => allResults
            };

            foreach (var r in allResults)
            {
                var trTimesZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
                var trTime = TimeZoneInfo.ConvertTimeFromUtc(r.CreatedAt, trTimeZone);
                Debug.WriteLine($"📄 CreatedAt (UTC): {r.CreatedAt} → TR: {trTime} (Filter: {filter})");
            }


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
