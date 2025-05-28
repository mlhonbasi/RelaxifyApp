using Microsoft.Extensions.DependencyInjection;
using Application.Services.Authentications.Login;
using Application.Services.Authentications.Register;
using Microsoft.Extensions.Configuration;
using Application.Services.Contents.MainContent;
using Application.Services.Contents.BreathingContents;
using Application.Services.Contents.MeditationContents;
using Application.Services.Contents.MusicContents;
using Application.Services.Contents.GameContent;
using Application.Services.Profile;
using Application.Services.Users;
using Domain.Interfaces;
using Application.Services.ContentLogs;
using Application.Services.Stress;
using Application.Services.StressTestResult;
using Application.Services.Goal;
using Application.Services.Achievement;
using Application.Services.Chatbot;

namespace Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<IBreathingContentService, BreathingContentService>();
            services.AddScoped<IMeditationContentService, MeditationContentService>();
            services.AddScoped<IMusicContentService, MusicContentService>();
            services.AddScoped<IGameContentService, GameContentService>();
            services.AddScoped<IUserContentLogService, UserContentLogService>();
            services.AddScoped<IStressService, StressService>();
            services.AddScoped<IStressTestResultService, StressTestResultService>();
            services.AddScoped<IUserGoalService, UserGoalService>();
            services.AddScoped<IAchievementService, AchievementService>();
            services.AddScoped<IContentFeedbackService, ContentFeedbackService>();
            services.AddScoped<IGeminiService, GeminiService>();

            return services;
        }
    }
}
