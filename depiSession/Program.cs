using depiSession.Data;
using depiSession.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace depiSession;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<AppDBContext>(opt =>
        {
            var connectionString = builder.Configuration.GetConnectionString("Default");
            opt.UseSqlServer(connectionString);
        }
        );

        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                // Disable password complexity requirements
                options.Password.RequireDigit = false;          // Requires 0-9
                options.Password.RequireLowercase = false;      // Requires lowercase letters
                options.Password.RequireUppercase = false;      // Requires uppercase letters
                options.Password.RequireNonAlphanumeric = false;// Requires special characters (e.g. !@#$)

                // Lower the minimum required length
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;       // Number of unique chars required
            })
            .AddEntityFrameworkStores<AppDBContext>();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Auth/Login";
            options.LogoutPath = "/Auth/Logout";
            options.AccessDeniedPath = "/Auth/Login";
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=student}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
