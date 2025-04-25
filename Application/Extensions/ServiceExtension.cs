using Microsoft.Extensions.DependencyInjection;
using Application.Services.Authentications.Login;
using Application.Services.Authentications.Register;
using Microsoft.Extensions.Configuration;
using Application.Services.Contents.Content;
using Application.Services.Contents.BreathingContent;
using Application.Services.Contents.MeditationContent;
using Application.Services.Contents.MusicContent;
using Application.Services.Contents.GameContent;

namespace Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();

            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<IBreathingContentService, BreathingContentService>();
            services.AddScoped<IMeditationContentService, MeditationContentService>();
            services.AddScoped<IMusicContentService, MusicContentService>();
            services.AddScoped<IGameContentService, GameContentService>();

            return services;
        }
    }
}
