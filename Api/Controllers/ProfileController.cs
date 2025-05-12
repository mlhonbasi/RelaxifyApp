using Application.Services.Profile;
using Application.Services.Profile.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController(IProfileService profileService) : ControllerBase
    {

        [HttpPut("ChangeName")]
        public async Task<IActionResult> UpdateFullName([FromBody] UpdateNameRequest request)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Surname))
                return BadRequest(new { message = "Ad Soyad boş olamaz." });

            await profileService.UpdateFullNameAsync(userId, request);
            return Ok(new { message = "Ad Soyad başarıyla güncellendi." });
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "Şifre boş olamaz." });

            await profileService.UpdatePasswordAsync(userId, request);
            return Ok(new { message = "Şifre başarıyla güncellendi." });
        }
        private Guid GetUserIdFromToken()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userIdStr!);
        }
    }
}
