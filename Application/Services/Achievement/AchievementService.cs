using Application.DTOs;
using Application.Services.Goal;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System.Globalization;

namespace Application.Services.Achievement
{
    public class AchievementService(
        IUserContentLogService logService,
        IUserGoalService goalService,
        IUserAchievementRepository achievementRepository) : IAchievementService
    {
        public async Task MarkNewAchievementsAsSeenAsync(Guid userId)
        {
            var unseenAchievements = await achievementRepository.GetUnseenAchievements(userId);

            if (unseenAchievements.Count == 0)
                return;

            foreach (var achievement in unseenAchievements)
            {
                achievement.IsSeenByUser = true;
            }
            foreach (var achievement in unseenAchievements)
            {
                await achievementRepository.UpdateAsync(achievement);
            }

        }
        public async Task<List<AchievementDto>> GetUserAchievementsAsync(Guid userId)
        {
            var logs = await logService.GetUserLogsAsync(userId);
            var goals = await goalService.GetGoalsForUserAsync(userId);
            var now = DateTime.UtcNow;

            // 🗂️ Kullanıcının mevcut kazandığı kalıcı rozetler
            var userAchievements = await achievementRepository.GetByUserIdAsync(userId);
            var mindPower = userAchievements.FirstOrDefault(x => x.Key == "MindPower");
            var meditationTime = logs
                .Where(x => x.Category == ContentCategory.Meditation)
                .Sum(x => x.DurationInSeconds) / 60;

            if (mindPower == null && meditationTime >= 100)
            {
                mindPower = new Domain.Entities.UserAchievement
                {
                    UserId = userId,
                    Key = "MindPower",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(mindPower);
            }

            // 🎵 Müzik Tutkunu – 10 farklı müzik içeriği
            var musicLover = userAchievements.FirstOrDefault(x => x.Key == "MusicLover");
            var uniqueMusicCount = logs
                .Where(x => x.Category == ContentCategory.Music)
                .Select(x => x.ContentId)
                .Distinct()
                .Count();

            if (musicLover == null && uniqueMusicCount >= 10)
            {
                musicLover = new Domain.Entities.UserAchievement
                {
                    UserId = userId,
                    Key = "MusicLover",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(musicLover);
            }

            // 🧘 Zihin Sessizliği – 5 farklı meditasyon
            var meditationMaster = userAchievements.FirstOrDefault(x => x.Key == "MeditationMaster");
            var uniqueMeditationCount = logs
                .Where(x => x.Category == ContentCategory.Meditation)
                .Select(x => x.ContentId)
                .Distinct()
                .Count();

            if (meditationMaster == null && uniqueMeditationCount >= 5)
            {
                meditationMaster = new Domain.Entities.UserAchievement
                {
                    UserId = userId,
                    Key = "MeditationMaster",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(meditationMaster);
            }

            // 🎮 Oyunla Rahatla – 3 farklı oyun
            var gameRelaxer = userAchievements.FirstOrDefault(x => x.Key == "GameRelaxer");
            var uniqueGames = logs
                .Where(x => x.Category == ContentCategory.Game)
                .Select(x => x.ContentId)
                .Distinct()
                .Count();

            if (gameRelaxer == null && uniqueGames >= 3)
            {
                gameRelaxer = new Domain.Entities.UserAchievement
                {
                    UserId = userId,
                    Key = "GameRelaxer",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(gameRelaxer);
            }

            // 🌈 Rahatlık Ustası – tüm içeriklerde toplam 500 dk
            var totalMaster = userAchievements.FirstOrDefault(x => x.Key == "TotalMaster");
            var totalMinutes = logs.Sum(x => x.DurationInSeconds) / 60;

            if (totalMaster == null && totalMinutes >= 500)
            {
                totalMaster = new Domain.Entities.UserAchievement
                {
                    UserId = userId,
                    Key = "TotalMaster",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(totalMaster);
            }
            // 🚀 YENİ BAŞLANGIÇ (kalıcı)
            var hasAnyGoal = goals.Any();
            var firstGoal = userAchievements.FirstOrDefault(x => x.Key == "FirstGoal");

            if (firstGoal == null && hasAnyGoal)
            {
                firstGoal = new Domain.Entities.UserAchievement
                {
                    UserId = userId,
                    Key = "FirstGoal",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(firstGoal);
            }

            // 👣 İLK ADIM (kalıcı)
            var hasUsedAnyContent = logs.Any();
            var firstStep = userAchievements.FirstOrDefault(x => x.Key == "FirstStep");

            if (firstStep == null && hasUsedAnyContent)
            {
                firstStep = new Domain.Entities.UserAchievement
                {
                    UserId = userId,
                    Key = "FirstStep",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(firstStep);
            }

            // 📊 Haftalık başarılar
            var weeklySuccesses = new HashSet<int>();
            foreach (var goal in goals)
            {
                var grouped = logs
                    .Where(x => x.Category == goal.Category)
                    .GroupBy(x => CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(x.CreatedAt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday));

                foreach (var week in grouped)
                {
                    var validDays = week
                        .GroupBy(x => x.CreatedAt.Date)
                        .Count(g => g.Sum(x => x.DurationInSeconds) >= goal.MinimumDailyMinutes * 60);

                    var minutesInThisWeek = week.Sum(x => x.DurationInSeconds) / 60;

                    if (validDays >= goal.TargetDays && minutesInThisWeek >= goal.TargetMinutes)
                    {
                        weeklySuccesses.Add(week.Key);
                    }
                }
            }

            var thisWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var bronze = userAchievements.FirstOrDefault(x => x.Key == "BronzeDiscipline");
            if (bronze == null && weeklySuccesses.Contains(thisWeek))
            {
                bronze = new UserAchievement
                {
                    UserId = userId,
                    Key = "BronzeDiscipline",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(bronze);
                userAchievements.Add(bronze); // listeyi güncelle
            }
            // 🥈 Gümüş İrade
            var silver = userAchievements.FirstOrDefault(x => x.Key == "SilverWill");

            if (silver == null &&
                weeklySuccesses.Contains(thisWeek) &&
                weeklySuccesses.Contains(thisWeek - 1) &&
                weeklySuccesses.Contains(thisWeek - 2))
            {
                silver = new UserAchievement
                {
                    UserId = userId,
                    Key = "SilverWill",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(silver);
                userAchievements.Add(silver);
            }


            var totalWeeks = weeklySuccesses.Count;
            var gold = userAchievements.FirstOrDefault(x => x.Key == "GoldHabit");

            if (gold == null && totalWeeks >= 5)
            {
                gold = new UserAchievement
                {
                    UserId = userId,
                    Key = "GoldHabit",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(gold);
                userAchievements.Add(gold);
            }

            var usedCategories = logs.Select(x => x.Category).Distinct().ToList();
            var hasUsedAll = usedCategories.Count >= 4;
            var balance = userAchievements.FirstOrDefault(x => x.Key == "BalanceMaster");

            if (balance == null && hasUsedAll)
            {
                balance = new UserAchievement
                {
                    UserId = userId,
                    Key = "BalanceMaster",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(balance);
                userAchievements.Add(balance);
            }


            var categoryWeekMap = logs
                .GroupBy(x => new
                {
                    x.Category,
                    Week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(x.CreatedAt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)
                })
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

            var series = userAchievements.FirstOrDefault(x => x.Key == "SeriesContinue");
            if (series == null && hasSeries)
            {
                series = new UserAchievement
                {
                    UserId = userId,
                    Key = "SeriesContinue",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(series);
                userAchievements.Add(series);
            }


            var successfulWeeks = weeklySuccesses.ToList();
            var hasComeback = successfulWeeks.Any(w => !successfulWeeks.Contains(w - 1) && successfulWeeks.Contains(w));
            var comeback = userAchievements.FirstOrDefault(x => x.Key == "Comeback");

            if (comeback == null && hasComeback)
            {
                comeback = new UserAchievement
                {
                    UserId = userId,
                    Key = "Comeback",
                    AchievedAt = now
                };
                await achievementRepository.AddAsync(comeback);
                userAchievements.Add(comeback);
            }


            // ✅ ROZETLERİ LİSTELE
            return new List<AchievementDto>
            {   
                new() {
                    Name = "Zihin Gücü",
                    Icon = "🧠",
                    Description = "Meditasyonda 100 dakika tamamladın.",
                    Achieved = mindPower != null,
                    AchievedAt = mindPower?.AchievedAt,
                    Type = "progress",
                    IsNewThisSession = mindPower != null && !mindPower.IsSeenByUser
                },
                new() {
                    Name = "Müzik Tutkunu",
                    Icon = "🎵",
                    Description = "10 farklı müzik içeriği dinledin.",
                    Achieved = musicLover != null,
                    AchievedAt = musicLover?.AchievedAt,
                    Type="content",
                    IsNewThisSession = musicLover != null && !musicLover.IsSeenByUser
                },
                new() {
                    Name = "Zihin Sessizliği",
                    Icon = "🧘",
                    Description = "5 farklı meditasyon tamamladın.",
                    Achieved = meditationMaster != null,
                    AchievedAt = meditationMaster?.AchievedAt,
                    Type="content",
                    IsNewThisSession = meditationMaster != null && !meditationMaster.IsSeenByUser
                },
                new() {
                    Name = "Oyunla Rahatla",
                    Icon = "🎮",
                    Description = "3 farklı oyun oynadın.",
                    Achieved = gameRelaxer != null,
                    AchievedAt = gameRelaxer?.AchievedAt,
                    Type="content",
                    IsNewThisSession = gameRelaxer != null && !gameRelaxer.IsSeenByUser
                },
                new() {
                    Name = "Rahatlık Ustası",
                    Icon = "🌈",
                    Description = "Toplamda 500 dakika içerik kullandın.",
                    Achieved = totalMaster != null,
                    AchievedAt = totalMaster?.AchievedAt,
                    Type = "progress",
                    IsNewThisSession = totalMaster != null && !totalMaster.IsSeenByUser
                },

                new() {
                    Name = "İlk Adım",
                    Icon = "👣",
                    Description = "İlk içeriği kullandın.",
                    Achieved = firstStep != null,
                    AchievedAt = firstStep?.AchievedAt,
                    Type = "beginner",
                    IsNewThisSession = firstStep != null && !firstStep.IsSeenByUser
                },
                new() {
                    Name = "Yeni Başlangıç",
                    Icon = "✨",
                    Description = "İlk hedefini oluşturdun.",
                    Achieved = firstGoal != null,
                    AchievedAt = firstGoal?.AchievedAt,
                    Type = "beginner",
                    IsNewThisSession = firstGoal != null && !firstGoal.IsSeenByUser
                },
                new() {
                    Name = "Bronz Disiplin",
                    Icon = "🥉",
                    Description = "1 hafta boyunca hedefini tamamladın.",
                    Achieved = bronze != null,
                    AchievedAt = bronze?.AchievedAt,
                    Type = "consistency",
                    IsNewThisSession = bronze != null && !bronze.IsSeenByUser
                },
                new() {
                    Name = "Gümüş İrade",
                    Icon = "🥈",
                    Description = "3 hafta üst üste hedefini tamamladın.",
                    Achieved = silver != null,
                    AchievedAt = silver?.AchievedAt,
                    Type = "consistency",
                    IsNewThisSession = silver != null && !silver.IsSeenByUser
                },
                new() {
                    Name = "Altın Alışkanlık",
                    Icon = "🥇",
                    Description = "5 farklı haftada hedef tamamladın.",
                    Achieved = gold != null,
                    AchievedAt = gold?.AchievedAt,
                    Type = "consistency",
                    IsNewThisSession = gold != null && !gold.IsSeenByUser
                },
                new() {
                    Name = "Denge Ustası",
                    Icon = "⚖️",
                    Description = "Tüm içerik türlerini en az 1 kez kullandın.",
                    Achieved = balance != null,
                    AchievedAt = balance?.AchievedAt,
                    Type = "content",
                    IsNewThisSession = balance != null && !balance.IsSeenByUser
                },
                new() {
                    Name = "Seriye Devam",
                    Icon = "🔁",
                    Description = "2 hafta üst üste aynı modülde içerik kullandın.",
                    Achieved = series != null,
                    AchievedAt = series?.AchievedAt,
                    Type = "consistency",
                    IsNewThisSession = series != null && !series.IsSeenByUser
                },
                new() {
                    Name = "Asla Pes Etme",
                    Icon = "🔄",
                    Description = "Başarısız haftadan sonra hedefini tamamlayarak geri döndün.",
                    Achieved = comeback != null,
                    AchievedAt = comeback?.AchievedAt,
                    Type = "consistency",
                    IsNewThisSession = comeback != null && !comeback.IsSeenByUser
                }
            };
        }
    }
}
