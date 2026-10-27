using System.Data.Common;
using depiSession.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace depiSession.Data;

public class AppDBContext(DbContextOptions<AppDBContext> options):IdentityDbContext<ApplicationUser>(options) {
    public DbSet<Student> Students{ get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);     
    }
}
