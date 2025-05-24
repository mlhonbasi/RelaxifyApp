using Application.Services.Goal;
using Application.Services.Goal.Models;
using Application.Services.Users;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserGoalController(IUserGoalService goalService, IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllGoals()
        {
            var userId = await userService.GetUserIdAsync();
            var goals = await goalService.GetGoalsForUserAsync(userId);
            return Ok(goals);
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> GetGoalByCategory(ContentCategory category)
        {
            var userId = await userService.GetUserIdAsync();
            var goal = await goalService.GetGoalByCategoryAsync(userId, category);

            if (goal == null)
                return NotFound(new { message = "Hedef bulunamadı" });

            return Ok(goal);
        }

        [HttpPost]
        public async Task<IActionResult> SetOrUpdateGoal([FromBody] SetUserGoalRequest request)
        {
            var userId = await userService.GetUserIdAsync();
            await goalService.SetOrUpdateGoalAsync(userId, request);
            return Ok(new { success = true, message = "Hedef kaydedildi." });
        }
    }
}
