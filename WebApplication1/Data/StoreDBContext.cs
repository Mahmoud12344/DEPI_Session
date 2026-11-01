using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class StoreDBContext(DbContextOptions<StoreDBContext> opt ):DbContext(opt) {
    public DbSet<Product> Products{ get; set; }
    public DbSet<Category> Categories{ get; set; }
    
}