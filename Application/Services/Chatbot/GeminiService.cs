using Application.Services.Chatbot.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services.Chatbot
{
    public class GeminiService(HttpClient httpClient, IConfiguration config) : IGeminiService
    {
        public async Task<string> AskGeminiAsync(GeminiRequestModel model)
        {
            var apiKey = config["Gemini:ApiKey"];
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";


            var body = new
            {
                contents = new[]
         {
            new
            {
                parts = new[]
                {
                    new
                    {
                        text = @$"
Kullanıcı şu anda {model.Page} sayfasında.

Bu sayfa içeriği şunları içeriyor:
{model.Context.ToString()}

Kullanıcının sorusu:
{model.UserMessage}

Lütfen sayfa verileriyle uyumlu, açıklayıcı ve destekleyici bir yanıt üret."
                    }
                }
            }
        }
            };

            var response = await httpClient.PostAsJsonAsync(url, body);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<JsonElement>();
            return result.GetProperty("candidates")[0].
                GetProperty("content").
                GetProperty("parts")[0].
                GetProperty("text").
                GetString();
        }
    }

}
