using Microsoft.EntityFrameworkCore;


namespace URLShortenerService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<URL> URLs { get; set; }
    }
}
