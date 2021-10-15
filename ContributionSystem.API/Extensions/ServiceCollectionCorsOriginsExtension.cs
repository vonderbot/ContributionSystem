using Microsoft.AspNetCore.Builder;

namespace ContributionSystem.API.Setup
{
    public static class ServiceCollectionCorsOriginsExtension
    {
        public static void SetCors(this IApplicationBuilder app)
        {
            app.UseCors("AllowLocalhost44308");
        }
    }
}
