using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Application.DTOs;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TtsController(HttpClient http, IConfiguration config) : ControllerBase
    {
        [HttpPost("speech")]
        public async Task<IActionResult> GetSpeech([FromBody] TtsRequestDto request)
        {
            //var apiKey = config["ElevenLabs:ApiKey"];
            //var voiceId = config["ElevenLabs:VoiceId"];

            //var apiUrl = $"https://api.elevenlabs.io/v1/text-to-speech/{voiceId}";

            //http.DefaultRequestHeaders.Clear();
            //http.DefaultRequestHeaders.Add("xi-api-key", apiKey);
            //http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("audio/mpeg"));

            //var payload = new
            //{
            //    text = request.Text,
            //    voice_settings = new
            //    {
            //        stability = 0.4,
            //        similarity_boost = 0.8
            //    }
            //};

            //var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            //var response = await http.PostAsync(apiUrl, content);
            //var audio = await response.Content.ReadAsByteArrayAsync();

            //return File(audio, "audio/mpeg");
            return Ok(new { message = "TTS service is not implemented yet." }); // Placeholder response for now
        }
    }
}
