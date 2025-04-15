using Microsoft.Extensions.DependencyInjection;
using Application.Services.Authentications.Login;
using Application.Services.Authentications.Register;
using Microsoft.Extensions.Configuration;

namespace Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            return services;
        }
    }
}
