using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SahilyBalloons.Website.Pages
{
    public class HeliumModel : PageModel
    {
        private readonly ILogger<HeliumModel> _logger;

        public HeliumModel(ILogger<HeliumModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
