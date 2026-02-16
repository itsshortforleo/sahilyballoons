using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SahilyBalloons.Website.Pages
{
    public class CorporateModel : PageModel
    {
        private readonly ILogger<CorporateModel> _logger;

        public CorporateModel(ILogger<CorporateModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
