using ContactManagement.Common.ServiceContracts;
using ContactManagementService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContactManagementService.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection RegisterMicroservices<TOptions>(this IServiceCollection services, IConfigurationSection section)
            where TOptions : class, IDictionary<string, string>
        {
            var values = section
                .GetChildren()
                .ToList();

            services.AddScoped((sp) => new ServiceProvider<IPhoneBookService>(values).Service);

            return services;
        }

        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("ContactManagementApi", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Contact Management API",
                    Description = "Contact Management API",

                    Contact = new OpenApiContact()
                    {
                        Name = "Contact Management",
                        Email = "papa.bengu@gmail.com"
                    }
                });

                var xmlPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "swagger.xml");

                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
