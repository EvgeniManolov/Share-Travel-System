namespace ShareTravelSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Web.Areas.Identity.Data;

    public class ShareTravelSystemDbContext : IdentityDbContext<ShareTravelSystemUser>
    {
        public ShareTravelSystemDbContext(DbContextOptions<ShareTravelSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Town> TownsAsDestination { get; set; }
        public DbSet<Town> TownsAsDeparture { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Offer>()
                .HasOne(s => s.DepartureTown)
                .WithMany(g => g.TownsAsDepartureTown)
                .HasForeignKey(s => s.DepartureTownId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Offer>()
                .HasOne(s => s.DestinationTown)
                .WithMany(g => g.TownsAsDestinationTown)
                .HasForeignKey(s => s.DestinationTownId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}