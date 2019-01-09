namespace ShareTravelSystem.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ShareTravelSystem.Services;
    using Web.Areas.Identity.Data;
    using Web.Models;
    using Xunit;

    public class StatisticServiceTests
    {
        private UserManager<ShareTravelSystemUser> UserManager { get; set; }

        public StatisticServiceTests()
        {
            UserManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task GetStatisticForAllUsersByRatingAsync_WithCorrectData_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var statisticsService = new StatisticService(context);
                // Create Offer and users
                var offerService = new OfferService(context, UserManager);

                var users = new List<ShareTravelSystemUser>
                {
                    new ShareTravelSystemUser {UserName = "TestUserr"},
                    new ShareTravelSystemUser {UserName = "TestUserr2"}
                };

                var towns = new List<Town>
                {
                    new Town {Name = "гр.Софияаа"},
                    new Town {Name = "гр.Варнааа"}
                };

                await context.Users.AddRangeAsync(users);
                await context.Towns.AddRangeAsync(towns);
                await context.SaveChangesAsync();

                var offer = new Offer
                {
                    Type = OfferType.Search,
                    DepartureTownId = 1,
                    DestinationTownId = 2,
                    Seat = 3,
                    Price = 5,
                    DepartureDate = DateTime.UtcNow,
                    Description = "Хубаво!",
                    Author = users[0],
                    TotalRating = 0,
                    CreateDate = DateTime.UtcNow
                };
                await context.Offers.AddAsync(offer);
                await context.SaveChangesAsync();

                // Act
                var returnedModel = await offerService.LikeOfferAsync(offer.Id, users[1].Id);
                var statistics = await statisticsService.GetStatisticForAllUsersByRatingAsync(0, null);
                var totalLikes = statistics.Statistic.Statistics.First().TotalLikes;

                //Assert
                Assert.Equal(2, statistics.Statistic.Statistics.Count);
                Assert.Equal(1, totalLikes);
            }
        }

        private static DbContextOptions<ShareTravelSystemDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ShareTravelSystemDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}