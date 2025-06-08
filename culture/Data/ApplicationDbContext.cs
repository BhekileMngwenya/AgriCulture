using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using culture.Models;
using Microsoft.AspNetCore.Identity;

namespace culture.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>  // Inherit from IdentityDbContext<IdentityUser, IdentityRole>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<FarmerProfile> Farmers { get; set; }
    }
}
