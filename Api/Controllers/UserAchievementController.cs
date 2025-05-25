using Application.DTOs;
using Application.Services.Authentications.Login;
using Application.Services.Goal;
using Application.Services.Users;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAchievementController(IUserService userService, IUserContentLogService logService, IUserGoalService userGoalService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAchievements()
        {
            var userId = await userService.GetUserIdAsync();
            var logs = await logService.GetUserLogsAsync(userId);
            var goals = await userGoalService.GetGoalsForUserAsync(userId);
            var hasAnyGoal = goals.Any();
            var hasUsedAnyContent = logs.Any();

            var now = DateTime.UtcNow;

            // 1. Haftalık başarıları hesapla (tarihsel haftaya göre grupla)
            var weeklySuccesses = new Dictionary<int, bool>(); // ISO hafta numarası

            foreach (var goal in goals)
            {
                var grouped = logs
                    .Where(x => x.Category == goal.Category)
                    .GroupBy(x => CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                        x.CreatedAt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
                    .ToList();

                foreach (var week in grouped)
                {
                    var validDays = week
                        .GroupBy(x => x.CreatedAt.Date)
                        .Count(g => g.Sum(x => x.DurationInSeconds) >= goal.MinimumDailyMinutes * 60);

                    var totalMinutes = week.Sum(x => x.DurationInSeconds) / 60;

                    if (validDays >= goal.TargetDays && totalMinutes >= goal.TargetMinutes)
                    {
                        weeklySuccesses[week.Key] = true;
                    }
                }
            }

            var thisWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var hasBronze = weeklySuccesses.ContainsKey(thisWeek);
            var hasSilver = weeklySuccesses.ContainsKey(thisWeek - 1) &&
                            weeklySuccesses.ContainsKey(thisWeek - 2) &&
                            weeklySuccesses.ContainsKey(thisWeek);
            var totalWeeks = weeklySuccesses.Count;
            var categoryWeekMap = logs
    .GroupBy(x => new { x.Category, Week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(x.CreatedAt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) })
    .ToDictionary(g => g.Key, g => g.ToList());

            bool hasSeries = false;

            foreach (var category in Enum.GetValues(typeof(ContentCategory)).Cast<ContentCategory>())
            {
                var weekNums = categoryWeekMap
                    .Where(kvp => kvp.Key.Category == category)
                    .Select(kvp => kvp.Key.Week)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                for (int i = 0; i < weekNums.Count - 1; i++)
                {
                    if (weekNums[i + 1] == weekNums[i] + 1)
                    {
                        hasSeries = true;
                        break;
                    }
                }
            }
            var successfulWeeks = weeklySuccesses.Keys.ToList();
            var hasComeback = successfulWeeks.Any(w => !successfulWeeks.Contains(w - 1) && successfulWeeks.Contains(w));


            // 2. Tüm kategoriler kullanıldı mı (Breathing, Music, Meditation, Game)
            var usedCategories = logs.Select(x => x.Category).Distinct().ToList();
            var hasUsedAll = usedCategories.Count >= 4;

            var result = new List<AchievementDto>
            {
                new()
                {
                    Name = "İlk Adım",
                    Icon = "👣",
                    Description = "İlk içeriği kullandın.",
                    Achieved = hasUsedAnyContent
                },

                new()
                {
                    Name = "Yeni Başlangıç",
                    Icon = "✨",
                    Description = "İlk hedefini oluşturdun.",
                    Achieved = hasAnyGoal
                },

                new()
                {
                    Name = "Bronz Disiplin",
                    Icon = "🥉",
                    Description = "1 hafta boyunca hedefini tamamladın.",
                    Achieved = hasBronze,
                    AchievedAt = hasBronze ? now : null
                },
                new()
                {
                    Name = "Gümüş İrade",
                    Icon = "🥈",
                    Description = "3 hafta üst üste hedefini tamamladın.",
                    Achieved = hasSilver
                },
                new()
                {
                    Name = "Altın Alışkanlık",
                    Icon = "🥇",
                    Description = "5 farklı haftada hedef tamamladın.",
                    Achieved = totalWeeks >= 5
                },
                new()
                {
                    Name = "Denge Ustası",
                    Icon = "⚖️",
                    Description = "Tüm içerik türlerini en az 1 kez kullandın.",
                    Achieved = hasUsedAll
                },
                new()
                {
                    Name = "Seriye Devam",
                    Icon = "🔁",
                    Description = "2 hafta üst üste aynı modülde içerik kullandın.",
                    Achieved = hasSeries
                },
                new()
                {
                    Name = "Asla Pes Etme",
                    Icon = "🔄",
                    Description = "Başarısız haftadan sonra hedefini tamamlayarak geri döndün.",
                    Achieved = hasComeback
                },

            };

            return Ok(result);
        }

    }

}
