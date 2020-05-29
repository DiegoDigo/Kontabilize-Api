using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Kontabilize.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddDependenceSwagger(this IServiceCollection services){
            
            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var item in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(item.GroupName, new OpenApiInfo
                    {
                        Title = $"Kontabilize Api {item.ApiVersion}",
                        Version = item.ApiVersion.ToString(),
                        Description =
                            "Kontabilize Rest Api dotnet 3.0 para consulta de instenções ou abetura de empresas.",
                        Contact = new OpenApiContact
                        {
                            Name = "Kontabilize SA",
                            Url = new Uri("https://kontabilize.com.br")
                        }
                    });
                }
            });

            return services;
        }
    }
}