using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DIYer.Data;
namespace DIYer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DIYerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DIYerContext") ?? throw new InvalidOperationException("Connection string 'DIYerContext' not found.")));

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            IWebHostEnvironment env = app.Services.GetRequiredService<IWebHostEnvironment>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapRazorPages();

            app.Run();
        }
    }
}