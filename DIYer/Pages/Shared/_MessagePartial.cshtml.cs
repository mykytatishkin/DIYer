using DIYer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DIYer.Pages.Shared
{
    public class _MessagePartialModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public ChatMessage ChatMessage { get; set; }
        public void OnGet()
        {

        }
    }
}
