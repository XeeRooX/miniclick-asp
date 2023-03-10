using Microsoft.EntityFrameworkCore;

namespace miniclick.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        public DbSet<Url> Urls { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Url>().HasIndex(u => u.Uuid).IsUnique();
        }
    }
}
