namespace ShareTravelSystem.Tests.Services
{
    using ShareTravelSystem.ViewModels.Town;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services;

    public class TownServicesTests
    {
        private UserManager<ShareTravelSystemUser> userManager { get; set; }

        public TownServicesTests()
        {
            userManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task ShouldCreateTownAndSeeDataInDatabase()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            TownService townService = new TownService(db);

            CrateTownViewModel model = new CrateTownViewModel { Name = "ИмеГрад" };

            // Act
            await townService.CreateTownAsync(model);

            // Assert
            Assert.True(await db.Towns.CountAsync() == 1);

            string dbTownName = await db.Towns.Select(x => x.Name).SingleOrDefaultAsync();
            string modelTownName = model.Name;

            Assert.Equal(dbTownName, modelTownName);
        }

        [Fact]
        public async Task ShouldDeleteTownAndSeeDeleteFlagSetOnTrueInDatabase()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            TownService townService = new TownService(db);

            Town town = new Town { Name = "ИзтритГрад" };
            await db.Towns.AddAsync(town);
            await db.SaveChangesAsync();

            int townId = await db.Towns.Select(x => x.Id).SingleOrDefaultAsync();

            // Act
            await townService.DeleteTownAsync(townId);

            // Assert
            Assert.True(await db.Towns.Where(x => x.IsDeleted).CountAsync() == 1);

            bool flag = await db.Towns.Select(x => x.IsDeleted).SingleOrDefaultAsync();
            bool variableTrue = true;
            Assert.Equal(variableTrue, flag);
        }

        [Fact]
        public async Task ShouldEditTownAndSeeEditedDataInDatabase()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            TownService townService = new TownService(db);

            Town town = new Town { Name = "ИзтритГрад" };
            await db.Towns.AddAsync(town);
            await db.SaveChangesAsync();

            int townId = await db.Towns.Select(x => x.Id).SingleOrDefaultAsync();

            EditTownViewModel editTown = new EditTownViewModel { Id = townId, Name = "ЕдитнатГрад" };

            // Act
            await townService.EditTownAsync(editTown);

            // Assert
            Assert.True(await db.Towns.CountAsync() == 1);

            string editTownNameInDatabase = await db.Towns.Select(x => x.Name).SingleOrDefaultAsync();
            Assert.Equal(editTownNameInDatabase, editTown.Name);
        }

        [Fact]
        public async Task ShouldAllReturnedTownsEqualToTownsInDatabase()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            TownService townService = new TownService(db);

            Town town1 = new Town { Name = "Град1" };
            Town town2 = new Town { Name = "Град2" };
            Town town3 = new Town { Name = "Град3" };
            Town town4 = new Town { Name = "Град4" };

            await db.Towns.AddRangeAsync(town1, town2, town3, town4);
            await db.SaveChangesAsync();

            await townService.DeleteTownAsync(town1.Id);
            int townsInDatabase = await db.Towns.CountAsync();

            // Act
            var result = await townService.GetAllTownsAsync(0, null);
            int returnedActiveTownsCount = result.Towns.Count();

            // Assert
            Assert.Equal(townsInDatabase - 1, returnedActiveTownsCount);
        }

        [Fact]
        public async Task ShouldGetCorrectTownToEdit()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            TownService townService = new TownService(db);

            Town town = new Town { Name = "Таун" };
            await db.Towns.AddAsync(town);
            await db.SaveChangesAsync();

            int townId = town.Id;

            // Act
            var model = await townService.GetTownToEditAsync(townId);

            // Assert
            Assert.Equal(model.Name, town.Name);
            Assert.Equal(model.Id, town.Id);
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
