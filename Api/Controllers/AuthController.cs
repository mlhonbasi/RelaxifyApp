using Application.Services.Authentications.Login;
using Application.Services.Authentications.Login.DTOs;
using Application.Services.Authentications.Register;
using Application.Services.Authentications.Register.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController(IRegisterService registerService, ILoginService loginService) : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                await registerService.RegisterAsync(request);
                return Ok();
            }
            catch
            {
                return BadRequest(new{ error="Şifreler eşleşmiyor"});
            }

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                await loginService.LoginAsync(loginRequest);
                return Ok();
            }
            catch
            {
                return BadRequest(new { error = "Kullanıcı bulunamadı!" });
            }
        }
    }
}
