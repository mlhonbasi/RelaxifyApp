using Application.DTOs;
using Application.Services.Goal;
using Application.Services.Users;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserContentLogController(IUserContentLogService logService, IUserService userService, IUserGoalService userGoalService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Log([FromBody] LogUsageRequest request)
        {
            var userId = await userService.GetUserIdAsync();

            await logService.LogUsageAsync(
                userId,
                request.ContentId,
                request.Category,
                request.DurationInSeconds
            );

            return Ok(new { success = true });
        }

        [HttpGet("getcategoryusage")]
        public async Task<IActionResult> GetCategoryUsage([FromQuery] string filter = "weekly")
        {
            var userId = await userService.GetUserIdAsync();
            var allLogs = await logService.GetUserLogsAsync(userId);

            var now = DateTime.UtcNow;
            var filteredLogs = filter.ToLower() switch
            {
                "daily" => allLogs.Where(x => x.CreatedAt >= now.Date && x.CreatedAt < now.Date.AddDays(1)),
                "weekly" => allLogs.Where(x => x.CreatedAt >= now.AddDays(-7)),
                "monthly" => allLogs.Where(x => x.CreatedAt >= now.AddMonths(-1)),
                _ => allLogs
            };

            var result = filteredLogs
                .GroupBy(x => x.Category)
                .Select(g => new
                {
                    Category = g.Key.ToString(),
                    TotalDuration = g.Sum(x => x.DurationInSeconds)
                })
                .ToList();

            return Ok(result);
        }

        [HttpGet("usagestats")]
        public async Task<ActionResult<UsageStatsDto>> GetUsageStats([FromQuery] string filter = "weekly")
        {
            var userId = await userService.GetUserIdAsync();
            var allLogs = await logService.GetUserLogsAsync(userId);

            var now = DateTime.UtcNow;
            var filteredLogs = filter.ToLower() switch
            {
                "daily" => allLogs.Where(x => x.CreatedAt >= now.Date && x.CreatedAt < now.Date.AddDays(1)),
                "weekly" => allLogs.Where(x => x.CreatedAt >= now.AddDays(-7)),
                "monthly" => allLogs.Where(x => x.CreatedAt >= now.AddMonths(-1)),
                _ => allLogs
            };

            if (!filteredLogs.Any())
            {
                return Ok(new UsageStatsDto
                {
                    TotalMinutes = 0,
                    MostUsedCategory = "Yok",
                    DailyAverageMinutes = 0
                });
            }

            var totalSeconds = filteredLogs.Sum(x => x.DurationInSeconds);
            var totalMinutes = totalSeconds / 60;

            var mostUsed = filteredLogs
                .GroupBy(x => x.Category)
                .OrderByDescending(g => g.Sum(x => x.DurationInSeconds))
                .FirstOrDefault()?.Key.ToString() ?? "Yok";

            var dailyDivisor = filter.ToLower() switch
            {
                "daily" => 1,
                "weekly" => 7,
                "monthly" => 30,
                _ => 7
            };

            var dailyAverage = totalSeconds / 60 / dailyDivisor;

            return Ok(new UsageStatsDto
            {
                TotalMinutes = totalMinutes,
                MostUsedCategory = mostUsed,
                DailyAverageMinutes = dailyAverage
            });
        }
        [HttpGet("progress")]
        public async Task<ActionResult<List<ProgressStatsDto>>> GetProgressStats()
        {
            var userId = await userService.GetUserIdAsync();
            var logs = await logService.GetUserLogsAsync(userId);
            var goals = await userGoalService.GetGoalsForUserAsync(userId);

            var now = DateTime.UtcNow;
            var last7Days = logs
                .Where(x => x.CreatedAt >= now.AddDays(-7))
                .ToList();

            var result = new List<ProgressStatsDto>();

            foreach (var goal in goals)
            {
                var categoryLogs = last7Days
                    .Where(l => l.Category == goal.Category)
                    .ToList();

                var usedDays = categoryLogs
                    .Select(l => l.CreatedAt.Date)
                    .Distinct()
                    .Count();

                var usedMinutes = categoryLogs.Sum(l => l.DurationInSeconds) / 60;

                result.Add(new ProgressStatsDto
                {
                    Category = goal.Category,
                    UsedDays = usedDays,
                    UsedMinutes = usedMinutes,
                    TargetDays = goal.TargetDays,
                    TargetMinutes = goal.TargetMinutes
                });
            }

            return Ok(result);
        }

    }
}
