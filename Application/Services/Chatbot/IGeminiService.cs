namespace Application.Services.Chatbot
{
    public interface IGeminiService
    {
        Task<string> AskGeminiAsync(string prompt);
    }
}
