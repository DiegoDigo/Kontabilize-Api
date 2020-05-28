using Kontabilize.Domain.UserContext.Services;
using Kontabilize.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kontabilize.Api.Configurations
{
    public static class ServiceConfigurations
    {
        public static IServiceCollection AddDependencyService(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            return services;
        }
    }
}