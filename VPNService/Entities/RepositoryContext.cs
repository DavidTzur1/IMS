using Microsoft.EntityFrameworkCore;
using VPNService.Entities.Models;

namespace VPNService.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Screening>? Screenings { get; set; }
        public DbSet<ScreeningItem>? ScreeningItems { get; set; }
    }
}