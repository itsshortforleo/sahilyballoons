using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SahilyBalloons.Website.Pages
{
    public class GarlandModel : PageModel
    {
        private readonly ILogger<GarlandModel> _logger;

        public GarlandModel(ILogger<GarlandModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
