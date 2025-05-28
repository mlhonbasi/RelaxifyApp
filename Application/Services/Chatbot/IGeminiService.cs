using Application.Services.Chatbot.Models;

namespace Application.Services.Chatbot
{
    public interface IGeminiService
    {
        Task<string> AskGeminiAsync(GeminiRequestModel model);
    }
}
