using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SahilyBalloons.Website.Pages
{
    public class StoreModel : PageModel
    {
        private readonly ILogger<StoreModel> _logger;

        public StoreModel(ILogger<StoreModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
