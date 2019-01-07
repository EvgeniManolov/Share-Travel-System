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
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class OfferServiceTests
    {
        private UserManager<ShareTravelSystemUser> userManager { get; set; }

        public OfferServiceTests()
        {
            userManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task ShouldCreateOfferAndSeeDataInDatabase()
        {

            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, userManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUserr"
                };

                var towns = new List<Town> { new Town { Name = "гр.Софияаа" }, new Town { Name = "гр.Варнааа" } };

                await context.Users.AddAsync(user);
                await context.Towns.AddRangeAsync(towns);
                await context.SaveChangesAsync();

                CreateOfferViewModel model = new CreateOfferViewModel
                {
                    Type = "Search",
                    DepartureTownId = 1,
                    DestinationTownId = 2,
                    Seat = 2,
                    Price = 10,
                    DepartureDate = DateTime.UtcNow,
                    Description = "Здравейте!"
                };

                // Act
                await offerService.CreateOfferAsync(model, user.Id);

                // Assert
                Assert.True(await context.Offers.CountAsync() == 1);

                string dbOfferAuthor = await context.Offers.Select(x => x.Author.UserName).SingleOrDefaultAsync();
                Assert.Equal(user.UserName, dbOfferAuthor);
            }
        }

        [Fact]
        public async Task ShouldDetailsOfferDisplayDataAsThisInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, userManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUserr"
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                var towns = new List<Town> { new Town { Name = "гр.София" }, new Town { Name = "гр.Варна" } };

                await context.Towns.AddRangeAsync(towns);
                await context.SaveChangesAsync();

                Offer offer = new Offer
                {
                    Type = OfferType.Search,
                    DepartureTownId = 1,
                    DestinationTownId = 2,
                    Seat = 2,
                    Price = 10,
                    DepartureDate = DateTime.UtcNow,
                    Description = "Здравейте!",
                    Author = user,
                    TotalRating = 0,
                    CreateDate = DateTime.UtcNow
                };

                await context.Offers.AddAsync(offer);
                await context.SaveChangesAsync();

                // Act
                var returnedModel = await offerService.DetailsOfferAsync(offer.Id);

                // Assert
                Assert.Equal(offer.Id, returnedModel.Id);
                Assert.Equal(offer.Description, returnedModel.Description);
                Assert.Equal(offer.Type.ToString(), returnedModel.Type);
                Assert.Equal(offer.DepartureDate, returnedModel.DepartureDate);
            }

        }

        [Fact]
        public async Task ShouldGetAllActiveOffersInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {

                OfferService offerService = new OfferService(context, userManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUserас"
                };

                var towns = new List<Town> { new Town { Name = "гр.асд" }, new Town { Name = "гр.асд" } };

                await context.Towns.AddRangeAsync(towns);
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                var offers = new List<Offer> {
                                new Offer
                                {
                                    Type = OfferType.Search,
                                    DepartureTownId = 1,
                                    DestinationTownId = 2,
                                    Seat = 15,
                                    Price = 15,
                                    DepartureDate = DateTime.UtcNow,
                                    Description = "Не!",
                                    Author = user,
                                    TotalRating = 0,
                                    CreateDate = DateTime.UtcNow
                                },

                                 new Offer
                                {
                                    Type = OfferType.Search,
                                    DepartureTownId = 1,
                                    DestinationTownId = 2,
                                    Seat = 7,
                                    Price = 13,
                                    DepartureDate = DateTime.UtcNow,
                                    Description = "Хелоу!",
                                    Author = user,
                                    TotalRating = 0,
                                    CreateDate = DateTime.UtcNow
                                }};
                await context.Offers.AddRangeAsync(offers);
                await context.SaveChangesAsync();

                // Act
                var returnedModel = await offerService.GetAllOffersAsync(false, null, null, user.Id, 0);

                int size = returnedModel.AllOffers.Offers.Count();
                // Assert
                Assert.Equal(2, size);
            }
        }

        [Fact]
        public async Task ShouldGetAllTownsInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                OfferService offerService = new OfferService(context, userManager);
                var towns = new List<Town> { new Town { Name = "гр.асд" }, new Town { Name = "гр.асд" } };

                await context.Towns.AddRangeAsync(towns);
                await context.SaveChangesAsync();

                // Act
                var returnedTowns = offerService.GetAllTowns();

                // Assert
                Assert.Equal(2, returnedTowns.Count());
            }
        }

        [Fact]
        public async Task ShouldGetCorrectOfferToEdit()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, userManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                var towns = new List<Town> { new Town { Name = "гр.София" }, new Town { Name = "гр.Варна" } };

                await context.Towns.AddRangeAsync(towns);
                await context.Users.AddAsync(user);
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
                var returnedModel = await offerService.GetOfferToEditAsync(offer.Id, user.Id);

                // Assert
                Assert.Equal(offer.Id, returnedModel.OfferModel.Id);
                Assert.Equal(offer.Description, returnedModel.OfferModel.Description);
                Assert.Equal(offer.DepartureDate, returnedModel.OfferModel.DepartureDate);
            }
        }

        [Fact]
        public async Task ShouldLikeOfferAndSeeRatingDataInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, userManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };
                var towns = new List<Town> { new Town { Name = "гр.София" }, new Town { Name = "гр.Варна" } };

                await context.Towns.AddRangeAsync(towns);
                await context.Users.AddAsync(user);
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
                var returnedModel = await offerService.LikeOfferAsync(offer.Id, user.Id);

                // Assert
                Assert.Equal(1, offer.TotalRating);
            }
        }

        [Fact]
        public async Task ShouldDisLikeOfferAndSeeRatingDataInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, userManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };
                var towns = new List<Town> { new Town { Name = "гр.София" }, new Town { Name = "гр.Варна" } };

                await context.Towns.AddRangeAsync(towns);
                await context.Users.AddAsync(user);
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
                var returnedModel = await offerService.DisLikeOfferAsync(offer.Id, user.Id);

                // Assert
                Assert.Equal(-1, offer.TotalRating);
            }
        }

        [Fact]
        public async Task ShouldGetReactionOffOfferAndSeeItInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, userManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };
                var towns = new List<Town> { new Town { Name = "гр.София" }, new Town { Name = "гр.Варна" } };

                await context.Towns.AddRangeAsync(towns);
                await context.Users.AddAsync(user);
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
                var returnedModel = await offerService.DisLikeOfferAsync(offer.Id, user.Id);


                // Act
                var flag = await context.Reactions.Where(r => r.OfferId == offer.Id).Select(o => o.Action).SingleOrDefaultAsync();

                // Assert
                Assert.False(flag);
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
