

namespace ShareTravelSystem.Tests.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services;
    using ShareTravelSystem.ViewModels.Offer;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class StatisticServiceTests
    {
        private UserManager<ShareTravelSystemUser> userManager { get; set; }

        public StatisticServiceTests()
        {
            userManager = TestStartup.UserManager;
        }


        [Fact]
        public async Task ShouldGetCorrectStatisticAndSeeDataInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                StatisticService statisticsService = new StatisticService(context);
                // Create Offer and users
                OfferService offerService = new OfferService(context, userManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUserr"
                };

                var user2 = new ShareTravelSystemUser
                {
                    UserName = "TestUserr2"
                };

                var towns = new List<Town> { new Town { Name = "гр.Софияаа" }, new Town { Name = "гр.Варнааа" } };

                await context.Users.AddAsync(user);
                await context.Users.AddAsync(user2);
                await context.Towns.AddRangeAsync(towns);
                await context.SaveChangesAsync();

                Offer offer = new Offer
                {
                    Type = OfferType.Search,
                    DepartureTownId = 1,
                    DestinationTownId = 2,
                    Seat = 3,
                    Price = 5,
                    DepartureDate = DateTime.UtcNow,
                    Description = "Хубаво!",
                    Author = user,
                    TotalRating = 0,
                    CreateDate = DateTime.UtcNow
                };
                await context.Offers.AddAsync(offer);
                await context.SaveChangesAsync();

                // Act
                var returnedModel = await offerService.LikeOfferAsync(offer.Id, user2.Id);
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
