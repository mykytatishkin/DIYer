using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DIYer.Data;
using DIYer.Models;
using System.Buffers.Text;

namespace DIYer.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly DIYer.Data.DIYerContext _context;
        private readonly IWebHostEnvironment _env;

        public RegistrationModel(DIYer.Data.DIYerContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ChatUser ChatUser { get; set; } = default!;

        [BindProperty]
        public IFormFile Avatar { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.ChatUser == null || ChatUser == null)
            {
                return Page();
            }

            if( Avatar != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Avatar.OpenReadStream().CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    byte[] bytes = ms.ToArray();
                    ChatUser.Avatar = Convert.ToBase64String(bytes);
                    ChatUser.CreatedAt = DateTime.Now;
                }
            }

            _context.ChatUser.Add(ChatUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Login");
        }
    }
}
