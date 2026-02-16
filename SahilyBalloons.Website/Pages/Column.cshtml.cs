using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SahilyBalloons.Website.Pages
{
    public class ColumnModel : PageModel
    {
        private readonly ILogger<ColumnModel> _logger;

        public ColumnModel(ILogger<ColumnModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
