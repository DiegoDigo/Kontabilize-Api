using Kontabilize.Domain.CompanyContext.Handlers;
using Kontabilize.Domain.UserContext.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Kontabilize.Api.Configurations
{
    public static class HandlerConfiguration
    {
        public static IServiceCollection AddDependencyHandler(this IServiceCollection services)
        {
            services.AddScoped<UserHandler, UserHandler>();
            services.AddScoped<ProfileHandler, ProfileHandler>();
            services.AddScoped<CompanyHandler, CompanyHandler>();
            return services;
        }
    }
}
