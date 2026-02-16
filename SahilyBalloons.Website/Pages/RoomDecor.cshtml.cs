using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SahilyBalloons.Website.Pages
{
    public class RoomDecorModel : PageModel
    {
        private readonly ILogger<RoomDecorModel> _logger;

        public RoomDecorModel(ILogger<RoomDecorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
