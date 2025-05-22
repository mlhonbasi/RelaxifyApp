using Application.DTOs;
using Application.Services.Users;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class UserContentLogController(IUserContentLogService logService, IUserService userService) : ControllerBase
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
        public async Task<IActionResult> GetCategoryUsage()
        {
            var userId = await userService.GetUserIdAsync();
            var result = await logService.GetCategoryUsageAsync(userId);
            return Ok(result);
        }
    }
}
