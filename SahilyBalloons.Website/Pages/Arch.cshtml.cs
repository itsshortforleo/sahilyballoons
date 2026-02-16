using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SahilyBalloons.Website.Pages
{
    public class ArchModel : PageModel
    {
        private readonly ILogger<ArchModel> _logger;

        public ArchModel(ILogger<ArchModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
