using Domain.Options;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // Add your repository registrations here
            // Example: services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<RelaxifyDbContext>(options =>
            {
                var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

                options.UseNpgsql(connectionString!.DefaultConnection, npgsqlOptionsAction =>
                {
                    npgsqlOptionsAction.MigrationsAssembly(typeof(RelaxifyDbContext).Assembly.FullName);
                });
            });

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<IBreathingContentRepository, BreathingContentRepository>();
            services.AddScoped<IMeditationContentRepository, MeditationContentRepository>();
            services.AddScoped<IMusicContentRepository, MusicContentRepository>();  
            services.AddScoped<IGameContentRepository, GameContentRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IUserContentLogRepository, UserContentLogRepository>();
            services.AddScoped<IStressAnswerRepository, StressAnswerRepository>();
            services.AddScoped<IStressQuestionRepository, StressQuestionRepository>();
            services.AddScoped<IStressTestResultRepository, StressTestResultRepository>();
            services.AddScoped<IUserGoalRepository, UserGoalRepository>();
            services.AddScoped<IUserAchievementRepository, UserAchievementRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
