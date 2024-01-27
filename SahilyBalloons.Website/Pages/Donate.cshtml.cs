using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SahilyBalloons.Website.Pages
{
    public class DonateModel : PageModel
    {
        private readonly ILogger<DonateModel> _logger;

        public DonateModel(ILogger<DonateModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}