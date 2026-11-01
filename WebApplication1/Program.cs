using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<StoreDBContext>(opt =>
            opt.UseSqlServer("Server=tcp:127.0.0.1,1433;Initial Catalog=product_category_debSession ;User ID=sa;Password=hamouhkmk-25mo;Encrypt=False;TrustServerCertificate=True")
        );
        
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // Ignores cyclical references and serializes the object graph safely
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()){
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

     
        app.MapControllers();
        app.Run();
    }
}