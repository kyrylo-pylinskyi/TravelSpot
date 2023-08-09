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
        public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();
        public DbSet<Spot> Spots => Set<Spot>();
        public DbSet<SpotAddress> SpotAddresses => Set<SpotAddress>();
        public DbSet<SpotCategory> SpotCategories => Set<SpotCategory>();
        public DbSet<SpotPhoto> SpotPhotos => Set<SpotPhoto>();
        public DbSet<SpotTag> SpotTags => Set<SpotTag>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<SpotRate> SpotRatings => Set<SpotRate>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Disable cascade delete SpotRates
            modelBuilder.Entity<SpotRate>()
                .HasOne(sr => sr.Spot)
                .WithMany(s => s.Rates)
                .HasForeignKey(sr => sr.SpotId)
                .OnDelete(DeleteBehavior.Restrict);

            // Disable cascade delete UserSubscriptions
            modelBuilder.Entity<UserSubscription>()
            .HasOne(s => s.Subscriber)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(s => s.SubscriberId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(s => s.TargetUser)
                .WithMany()
                .HasForeignKey(s => s.TargetUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}