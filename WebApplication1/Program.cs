using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApplication1.Data;

namespace WebApplication1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddAuthorization();

        builder.Services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                // Ignores cyclical references and serializes the object graph safely
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        // Swagger & OpenAPI configuration for the API
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Database context
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? "Server=tcp:127.0.0.1,1433;Initial Catalog=product_category_debSession;User ID=sa;Password=hamouhkmk-25mo;Encrypt=False;TrustServerCertificate=True";

        builder.Services.AddDbContext<StoreDBContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
            opt.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API v1");
            });
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();

        // Default MVC route (Product catalog UI)
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Product}/{action=Index}/{id?}");

        // REST API controllers route (e.g. /api/products)
        app.MapControllers();

        app.Run();
    }
}
