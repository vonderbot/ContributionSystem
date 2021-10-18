using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ContributionSystem.API.Setup
{
    public static class ServiceCollectionCorsOriginsExtension
    {

        public static void ConfigureCorsForOrigins(this IServiceCollection services, IConfiguration configuration)
        {
            var hosts = configuration.GetSection("CorsOrigin:Links").Get<List<string>>().ToArray();
            var p = configuration.GetSection("CorsOrigin:AllowOrigins").Value;
            services.AddCors(options =>
            {
                options.AddPolicy(configuration.GetSection("CorsOrigin:AllowOrigins").Value, p =>
                {
                    p.WithOrigins(hosts)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
    }
}
