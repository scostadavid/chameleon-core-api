using Microsoft.EntityFrameworkCore;
using ChameleonCoreAPI.Domain;

namespace ChameleonCoreAPI.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ShortUrl> ShortUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShortUrl>()
                .HasIndex(s => s.ShortCode)
                .IsUnique();
        }
    }
}
