using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SahilyBalloons.Website.Pages
{
    public class WallModel : PageModel
    {
        private readonly ILogger<WallModel> _logger;

        public WallModel(ILogger<WallModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
