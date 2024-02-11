using Microsoft.EntityFrameworkCore;
using NZApi.Models.Domain;

namespace NZApi.Data
{
    public class NZDbContext : DbContext
    {
        public NZDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
