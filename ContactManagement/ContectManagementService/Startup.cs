using ContactManagementService.Extensions;
using ContactManagementService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManagementService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration).AddControllers();
            services.RegisterMicroservices<MicroServiceSettings>(Configuration.GetSection(nameof(MicroServiceSettings)))
                    .AddCors(o => o.AddPolicy("MyPolicy", builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .WithMethods("GET", "POST", "HEAD", "PUT")
                            .AllowAnyHeader();
                    }))
                    .AddOptions()
                    .RegisterSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");
            app.UseDeveloperExceptionPage()
               .UseRouting()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
