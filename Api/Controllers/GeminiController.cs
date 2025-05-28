using Application.Services.Chatbot;
using Application.Services.Chatbot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeminiController(IGeminiService geminiService) : ControllerBase
    {
        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] GeminiRequestModel model)
        {
            var answer = await geminiService.AskGeminiAsync(model.Prompt);
            return Ok(new { response = answer });
        }
    }
}
