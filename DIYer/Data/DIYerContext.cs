using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DIYer.Models;

namespace DIYer.Data
{
    public class DIYerContext : DbContext
    {
        public DIYerContext (DbContextOptions<DIYerContext> options)
            : base(options)
        {
        }

        public DbSet<DIYer.Models.ChatUser> ChatUser { get; set; } = default!;
        public DbSet<DIYer.Models.ChatMessage> ChatMessage { get; set; } = default!;
    }
}
