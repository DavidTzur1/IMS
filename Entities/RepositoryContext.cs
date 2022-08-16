using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
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