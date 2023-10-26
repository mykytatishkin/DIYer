using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DIYer.Data;
using DIYer.Models;

namespace DIYer.Pages
{
    public class LoginModel : PageModel
    {
        private readonly DIYer.Data.DIYerContext _context;

        public LoginModel(DIYer.Data.DIYerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ChatUser ChatUser { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            ChatUser chatUser = _context.ChatUser.Where( u => u.Email == ChatUser.Email && 
                u.Password == ChatUser.Password).FirstOrDefault();

            if (chatUser == null)
            {
                return RedirectToPage("./Registration");
            }
            HttpContext.Session.SetInt32("loggedId", chatUser.Id);
            HttpContext.Session.SetString("loggedNick", chatUser.Nick);
            return RedirectToPage("./Index");
        }
    }
}
