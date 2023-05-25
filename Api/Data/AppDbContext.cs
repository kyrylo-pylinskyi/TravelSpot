using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Spot> Spots => Set<Spot>();
        public DbSet<SpotAddress> SpotAddresses => Set<SpotAddress>();
        public DbSet<SpotCategory> SpotCategories => Set<SpotCategory>();
        public DbSet<SpotPhoto> SpotPhotos => Set<SpotPhoto>();
        public DbSet<SpotTag> SpotTags => Set<SpotTag>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<SpotRating> SpotRatings => Set<SpotRating>();
    }
}
