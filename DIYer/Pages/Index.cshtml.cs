using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DIYer.Data;
using DIYer.Models;

namespace DIYer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DIYer.Data.DIYerContext _context;
        private readonly IWebHostEnvironment _env;

        public IndexModel(DIYer.Data.DIYerContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty(SupportsGet = true)]
        public IList<MessageViewModel> ChatMessages { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public ChatUser User { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public ChatMessage Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public IFormFile FileUrl { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int? loggedId = HttpContext.Session.GetInt32("loggedId");
            if (loggedId == null)
            {
                return RedirectToPage("./Login");
            }

            User = await _context.ChatUser.FindAsync(loggedId);
            if (_context.ChatMessage != null)
            {
                var messages = await _context.ChatMessage.ToListAsync();
                foreach (var message in messages)
                {
                    var user = await _context.ChatUser.FindAsync(message.UserId);
                    ChatMessages.Add(new MessageViewModel() { Message = message, User = user });
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddMessage()
        {
            string fileUrl = string.Empty;
            string fileName = string.Empty;
            if(FileUrl != null)
            {
                fileName = FileUrl.FileName;
                string path = Path.Combine(_env.WebRootPath, "files", fileName);
                var stream = new FileStream(path, FileMode.Create);
                await FileUrl.CopyToAsync(stream);
                fileUrl = "/files/" + fileName;
            }

            ChatMessage message = new ChatMessage() {
                MessageText = Message.MessageText,
                FileName = fileName,
                FileUrl = fileUrl,
                UserId = (int)HttpContext.Session.GetInt32("loggedId"),
                CreatedAt = DateTime.Now,
                Likes = 0,
                Dislikes = 0
            };
            _context.ChatMessage.Add(message);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
