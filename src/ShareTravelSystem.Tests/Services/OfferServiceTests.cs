namespace ShareTravelSystem.Tests.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services;
    using ShareTravelSystem.ViewModels.Offer;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Linq;
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
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            OfferService offerService = new OfferService(db, userManager);

            var user = new ShareTravelSystemUser
            {
                UserName = "TestUser"
            };

            var town1 = new Town { Name = "гр.София" };
            var town2 = new Town { Name = "гр.Варна" };

            await db.Users.AddAsync(user);
            await db.Towns.AddRangeAsync(town1, town2);
            await db.SaveChangesAsync();

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
            Assert.True(await db.Offers.CountAsync() == 1);

            string dbOfferAuthor = await db.Offers.Select(x => x.Author.UserName).SingleOrDefaultAsync();
            Assert.Equal(user.UserName, dbOfferAuthor);
        }

        [Fact]
        public async Task ShouldDetailsOfferDisplayDataAsThisInDatabase()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            OfferService offerService = new OfferService(db, userManager);

            var user = new ShareTravelSystemUser
            {
                UserName = "TestUser"
            };
            var town1 = new Town { Name = "гр.София" };
            var town2 = new Town { Name = "гр.Варна" };
            
            await db.Towns.AddRangeAsync(town1, town2);
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

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

            await db.Offers.AddAsync(offer);
            await db.SaveChangesAsync();

            // Act
            var returnedModel = await offerService.DetailsOfferAsync(offer.Id);

            // Assert
            Assert.Equal(offer.Id, returnedModel.Id);
            Assert.Equal(offer.Description, returnedModel.Description);
            Assert.Equal(offer.Type.ToString(), returnedModel.Type);
            Assert.Equal(offer.DepartureDate, returnedModel.DepartureDate);
        }


        private ShareTravelSystemDbContext GetContext()
        {
            var dbOptions = new DbContextOptionsBuilder<ShareTravelSystemDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            return new ShareTravelSystemDbContext(dbOptions);
        }
    }
}
