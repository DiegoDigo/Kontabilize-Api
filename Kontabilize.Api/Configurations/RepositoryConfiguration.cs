using Kontabilize.Domain.UserContext.Repositories;
using Kontabilize.Infra.Context;
using Kontabilize.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Kontabilize.Api.Configurations
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddDependencyRepository(this IServiceCollection services)
        {
            services.AddScoped<KontabilizeContext, KontabilizeContext>();
            
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        } 
    }
}