using Application.DTOs;
using Application.Services.Achievement;
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
    public class UserAchievementController(IUserService userService, IAchievementService achievementService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAchievements()
        {
            var userId = await userService.GetUserIdAsync();
            var achievements = await achievementService.GetUserAchievementsAsync(userId);
            return Ok(achievements);
        }
        [HttpPost("mark-seen")]
        public async Task<IActionResult> MarkAchievementsAsSeen()
        {
            var userId = await userService.GetUserIdAsync();
            await achievementService.MarkNewAchievementsAsSeenAsync(userId);
            return NoContent(); // 204: başarıyla işlendi, ama veri yok
        }

    }

}
