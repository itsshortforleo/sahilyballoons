using Microsoft.EntityFrameworkCore;
using SahilyBalloons.Data;

namespace SahilyBalloons.Website.Configuration
{
    public static class AppServices
    {
        public static void AddDefaultServices(this WebApplicationBuilder webApplicationBuilder)
        {
            // Add services to the container.
            webApplicationBuilder.Services.AddRazorPages();
            webApplicationBuilder.Services.AddDbContextPool<SahilyBalloonsWebsiteDbContext>(options =>
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("SahilyBalloonsWebsiteDB")));
        }
    }
}
