using Api.Models.Entities.Application;
using Api.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserPhoto> UserPhotos => Set<UserPhoto>();
        public DbSet<Spot> Spots => Set<Spot>();
        public DbSet<SpotAddress> SpotAddresses => Set<SpotAddress>();
        public DbSet<SpotCategory> SpotCategories => Set<SpotCategory>();
        public DbSet<SpotPhoto> SpotPhotos => Set<SpotPhoto>();
        public DbSet<SpotTag> SpotTags => Set<SpotTag>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<SpotRating> SpotRatings => Set<SpotRating>();
    }
}