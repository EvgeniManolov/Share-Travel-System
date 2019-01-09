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
    using ShareTravelSystem.ViewModels;
    using ViewModels.Offer;
    using Web.Areas.Identity.Data;
    using Web.Models;
    using Xunit;

    public class OfferServiceTests
    {
        private UserManager<ShareTravelSystemUser> UserManager { get; set; }

        public OfferServiceTests()
        {
            this.UserManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task CreateOfferAsync_WithCorrectData_WorksCorrectly()
        {

            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, this.UserManager);

                ShareTravelSystemUser user = new ShareTravelSystemUser
                {
                    UserName = "TestUserr"
                };

                List<Town> towns = new List<Town> { new Town { Name = "гр.Софияаа" }, new Town { Name = "гр.Варнааа" } };

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
        public async Task DetailsOfferAsync_WithCorrectId_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, this.UserManager);

                ShareTravelSystemUser user = new ShareTravelSystemUser
                {
                    UserName = "TestUserr"
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                List<Town> towns = new List<Town> { new Town { Name = "гр.София" }, new Town { Name = "гр.Варна" } };

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
        public async Task GetAllOffersAsync_WithCorrectData_ReturnsAllActiveOffers()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {

                OfferService offerService = new OfferService(context, this.UserManager);

                ShareTravelSystemUser user = new ShareTravelSystemUser
                {
                    UserName = "TestUserас"
                };

                List<Town> towns = new List<Town> { new Town { Name = "гр.асд" }, new Town { Name = "гр.асд" } };

                await context.Towns.AddRangeAsync(towns);
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                List<Offer> offers = new List<Offer> {
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
                OfferPaginationViewModel returnedModel = await offerService.GetAllOffersAsync(false, null, null, user.Id, 0);

                int size = returnedModel.AllOffers.Offers.Count();

                // Assert
                Assert.Equal(2, size);
            }
        }

        [Fact]
        public async Task GetAllTowns_WithCorrectData_ReturnsAllActiveTowns()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                OfferService offerService = new OfferService(context, this.UserManager);
                List<Town> towns = new List<Town> { new Town { Name = "гр.асд" }, new Town { Name = "гр.асд" } };

                await context.Towns.AddRangeAsync(towns);
                await context.SaveChangesAsync();

                // Act
                IEnumerable<Town> returnedTowns = offerService.GetAllTowns();

                // Assert
                Assert.Equal(2, returnedTowns.Count());
            }
        }

        [Fact]
        public async Task GetOfferToEditAsync_WithExistsId_ReturnsCorrectOffer()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, this.UserManager);

                ShareTravelSystemUser user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                List<Town> towns = new List<Town> { new Town { Name = "гр.София" }, new Town { Name = "гр.Варна" } };

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
                DisplayEditOfferViewModel returnedModel = await offerService.GetOfferToEditAsync(offer.Id, user.Id);

                // Assert
                Assert.Equal(offer.Id, returnedModel.OfferModel.Id);
                Assert.Equal(offer.Description, returnedModel.OfferModel.Description);
                Assert.Equal(offer.DepartureDate, returnedModel.OfferModel.DepartureDate);
            }
        }

        [Fact]
        public async Task GetOfferToEditAsync_WithWrongUserId_ReturnsAndCatchException()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, this.UserManager);

                List<ShareTravelSystemUser> users = new List<ShareTravelSystemUser> { new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                }, new ShareTravelSystemUser
                {
                    UserName = "TestUser2"
                }};

                var towns = new List<Town> { new Town { Name = "гр.София" }, new Town { Name = "гр.Варна" } };

                await context.Towns.AddRangeAsync(towns);
                await context.Users.AddRangeAsync(users);
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
                    Author = users[0],
                    TotalRating = 0,
                    CreateDate = DateTime.UtcNow
                };

                await context.Offers.AddAsync(offer);
                await context.SaveChangesAsync();


                // Act
                string result = null;
                try
                {
                    var returnedModel = await offerService.GetOfferToEditAsync(offer.Id, users[1].Id);
                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                var exp = "User with id: " + users[1].Id + " is not authorized for this operation.";
                // Assert
                Assert.Equal(result, exp);
            }
        }

        [Fact]
        public async Task EditOfferAsync_WithCorrectModel_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, this.UserManager);

                ShareTravelSystemUser user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                 List<Town> towns = new List<Town> {
                     new Town { Name = "гр.София" },
                     new Town { Name = "гр.Варна" },
                     new Town { Name = "гр.Каварна"} };

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

                ICollection<Town> townsList = new List<Town>();
                townsList = context.Towns.ToList();

                EditOfferViewModel editOfferModel = new EditOfferViewModel
                {
                    Id = 1,
                    Type = "Search",
                    DepartureTownId = offer.DepartureTownId,
                    DepartureTownName = "гр.София",
                    DestinationTownId = offer.DestinationTownId,
                    DestinationTownName = "гр.Варна",
                    Seat = 4,
                    Price = 3,
                    DepartureDate = offer.DepartureDate,
                    Description = offer.Description

                };
                DisplayEditOfferViewModel result = new DisplayEditOfferViewModel { OfferModel = editOfferModel, Towns = townsList };
                // Act
                await offerService.EditOfferAsync(result);

                // Assert
                int seatDb = await context.Offers.Select(x => x.Seat).SingleOrDefaultAsync();
                decimal priceDb = await context.Offers.Select(x => x.Price).SingleOrDefaultAsync();
                Assert.Equal(editOfferModel.Seat, seatDb);
                Assert.Equal(editOfferModel.Price, priceDb);
            }
        }

        [Fact]
        public async Task LikeOfferAsync_WithCorrectData_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, this.UserManager);

                ShareTravelSystemUser user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };
                List<Town> towns = new List<Town> { new Town { Name = "гр.София" }, new Town { Name = "гр.Варна" } };

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
                bool returnedModel = await offerService.LikeOfferAsync(offer.Id, user.Id);

                // Assert
                Assert.Equal(1, offer.TotalRating);
            }
        }

        [Fact]
        public async Task DisLikeOfferAsync_WithCorrectData_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, this.UserManager);

                ShareTravelSystemUser user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                List<Town> towns = new List<Town> { new Town { Name = "гр.София" }, new Town { Name = "гр.Варна" } };

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
                bool returnedModel = await offerService.DisLikeOfferAsync(offer.Id, user.Id);

                // Assert
                Assert.Equal(-1, offer.TotalRating);
            }
        }

        [Fact]
        public async Task GetReactions_WithCorrectData_CountCorrectlyReactions()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, this.UserManager);

                ShareTravelSystemUser user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                List<Town> towns = new List<Town> { new Town { Name = "гр.София" }, new Town { Name = "гр.Варна" } };

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
                bool returnedModel = await offerService.DisLikeOfferAsync(offer.Id, user.Id);


                // Act
                bool flag = await context.Reactions.Where(r => r.OfferId == offer.Id).Select(o => o.Action).SingleOrDefaultAsync();

                // Assert
                Assert.False(flag);
            }
        }

        [Fact]
        public async Task DeleteOfferAsync_WithCorrectId_WorksCorrectly()
        {

            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                OfferService offerService = new OfferService(context, this.UserManager);

                ShareTravelSystemUser user = new ShareTravelSystemUser
                {
                    UserName = "TestUserr"
                };

                List<Town> towns = new List<Town> { new Town { Name = "гр.Софияаа" }, new Town { Name = "гр.Варнааа" } };

                await context.Users.AddAsync(user);
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
                await offerService.DeleteOfferAsync(offer.Id);

                // Assert
                Assert.True(await context.Offers.Select(x => x.IsDeleted).SingleOrDefaultAsync());
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
