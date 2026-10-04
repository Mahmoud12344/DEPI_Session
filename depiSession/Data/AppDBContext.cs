using System.Data.Common;
using depiSession.Models;
using Microsoft.EntityFrameworkCore;

namespace depiSession.Data;

public class AppDBContext:DbContext {
    public DbSet<Student> Students{ get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            "Data Source=tcp:127.0.0.1,1433;Initial Catalog=DepiSession ;User ID=sa;Password=hamouhkmk-25mo;Encrypt=False;TrustServerCertificate=True");
    }
}
