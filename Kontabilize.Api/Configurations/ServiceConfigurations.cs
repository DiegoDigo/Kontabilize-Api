using Kontabilize.Domain.CompanyContext.Services;
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
            services.AddTransient<IUploadService, UploadService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IProfileService, ProfileService>();
            return services;
        }
    }
}
