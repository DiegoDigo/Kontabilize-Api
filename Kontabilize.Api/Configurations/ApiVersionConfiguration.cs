using Microsoft.Extensions.DependencyInjection;

namespace Kontabilize.Api.Configurations
{
    public static class ApiVersionConfiguration
    {
        public static IServiceCollection AddDependencyApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning();
            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            });
            
            return services;
        }
        
    }
}