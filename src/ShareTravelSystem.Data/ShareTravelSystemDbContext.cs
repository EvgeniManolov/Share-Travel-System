using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShareTravelSystem.Data.Models;
using ShareTravelSystem.Web.Areas.Identity.Data;

namespace ShareTravelSystem.Web.Models
{
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);

            base.OnModelCreating(builder);
            builder.Entity<Offer>()
            .HasOne<Town>(s => s.DepartureTown)
            .WithMany(g => g.TownsAsDepartureTown)
            .HasForeignKey(s => s.DepartureTownId);

            builder.Entity<Offer>()
                        .HasOne<Town>(s => s.DestinationTown)
                        .WithMany(g => g.TownsAsDestinationTown)
                        .HasForeignKey(s => s.DestinationTownId);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
