using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ContributionSystem.API.Setup
{
    public static class ServiceCollectionCorsOriginsExtension
    {
        public static void SetCors(this IApplicationBuilder app)
        {
            app.UseCors("AllowedOrigins");
        }

        public static void CreateCors(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            var AppConfiguration = builder.Build();
            var hosts = AppConfiguration.GetSection("AllowedOrigins").Get<List<string>>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowedOrigins", p =>
                {
                    p.WithOrigins(hosts.ToArray())
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
    }
}
