﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShareTravelSystem.Web.Models;

namespace ShareTravelSystem.Data.Migrations
{
    [DbContext(typeof(ShareTravelSystemDbContext))]
    [Migration("20181229195735_reaction")]
    partial class reaction
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ShareTravelSystem.Data.Models.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("ShareTravelSystem.Data.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime>("DepartureDate");

                    b.Property<int>("DepartureTownId");

                    b.Property<string>("Description");

                    b.Property<int>("DestinationTownId");

                    b.Property<decimal>("Price");

                    b.Property<int>("Seat");

                    b.Property<int>("TotalRating");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("DepartureTownId");

                    b.HasIndex("DestinationTownId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("ShareTravelSystem.Data.Models.Reaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Action");

                    b.Property<string>("AuthorId");

                    b.Property<int>("OfferId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("OfferId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("ShareTravelSystem.Data.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorId");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("OfferId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("OfferId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ShareTravelSystem.Data.Models.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("ShareTravelSystem.Web.Areas.Identity.Data.ShareTravelSystemUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ShareTravelSystem.Web.Areas.Identity.Data.ShareTravelSystemUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ShareTravelSystem.Web.Areas.Identity.Data.ShareTravelSystemUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShareTravelSystem.Web.Areas.Identity.Data.ShareTravelSystemUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ShareTravelSystem.Web.Areas.Identity.Data.ShareTravelSystemUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShareTravelSystem.Data.Models.Announcement", b =>
                {
                    b.HasOne("ShareTravelSystem.Web.Areas.Identity.Data.ShareTravelSystemUser", "Author")
                        .WithMany("Announcements")
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("ShareTravelSystem.Data.Models.Offer", b =>
                {
                    b.HasOne("ShareTravelSystem.Web.Areas.Identity.Data.ShareTravelSystemUser", "Author")
                        .WithMany("Offers")
                        .HasForeignKey("AuthorId");

                    b.HasOne("ShareTravelSystem.Data.Models.Town", "DepartureTown")
                        .WithMany("TownsAsDepartureTown")
                        .HasForeignKey("DepartureTownId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ShareTravelSystem.Data.Models.Town", "DestinationTown")
                        .WithMany("TownsAsDestinationTown")
                        .HasForeignKey("DestinationTownId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ShareTravelSystem.Data.Models.Reaction", b =>
                {
                    b.HasOne("ShareTravelSystem.Web.Areas.Identity.Data.ShareTravelSystemUser", "Author")
                        .WithMany("Reactions")
                        .HasForeignKey("AuthorId");

                    b.HasOne("ShareTravelSystem.Data.Models.Offer", "Offer")
                        .WithMany("Reactions")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShareTravelSystem.Data.Models.Review", b =>
                {
                    b.HasOne("ShareTravelSystem.Web.Areas.Identity.Data.ShareTravelSystemUser", "Author")
                        .WithMany("Reviews")
                        .HasForeignKey("AuthorId");

                    b.HasOne("ShareTravelSystem.Data.Models.Offer", "Offer")
                        .WithMany("Reviews")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
