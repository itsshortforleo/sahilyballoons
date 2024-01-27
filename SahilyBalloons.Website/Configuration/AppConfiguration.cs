namespace SahilyBalloons.Website.Configuration
{
    public static class AppConfiguration
    {
        public static void AddDefaultConfiguration( this WebApplication webApplication)
        {
            // Configure the HTTP request pipeline.
            if (!webApplication.Environment.IsDevelopment())
            {
                webApplication.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                webApplication.UseHsts();
            }

            webApplication.UseHttpsRedirection();
            webApplication.UseStaticFiles();

            webApplication.UseRouting();

            webApplication.UseAuthorization();

            webApplication.MapRazorPages();
        }
    }
}
