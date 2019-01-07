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
    using Microsoft.Extensions.DependencyInjection;

    public class TownServiceTests
    {
        private UserManager<ShareTravelSystemUser> userManager { get; set; }

        public TownServiceTests()
        {
            userManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task ShouldCreateTownAndSeeDataInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                TownService townService = new TownService(context);

                CrateTownViewModel model = new CrateTownViewModel { Name = "ИмеГрад" };

                // Act
                await townService.CreateTownAsync(model);

                // Assert
                Assert.True(await context.Towns.CountAsync() == 1);

                string dbTownName = await context.Towns.Select(x => x.Name).SingleOrDefaultAsync();
                string modelTownName = model.Name;

                Assert.Equal(dbTownName, modelTownName);
            }
        }

        [Fact]
        public async Task ShouldDeleteTownAndSeeDeleteFlagSetOnTrueInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                TownService townService = new TownService(context);

                Town town = new Town { Name = "ИзтритГрад" };
                await context.Towns.AddAsync(town);
                await context.SaveChangesAsync();

                int townId = await context.Towns.Select(x => x.Id).SingleOrDefaultAsync();

                // Act
                await townService.DeleteTownAsync(townId);

                // Assert
                Assert.True(await context.Towns.Where(x => x.IsDeleted).CountAsync() == 1);
                bool flag = await context.Towns.Select(x => x.IsDeleted).SingleOrDefaultAsync();
                Assert.True(flag);
            }
        }

        [Fact]
        public async Task ShouldEditTownAndSeeEditedDataInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                TownService townService = new TownService(context);

                Town town = new Town { Name = "ИзтритГрад" };
                await context.Towns.AddAsync(town);
                await context.SaveChangesAsync();

                int townId = await context.Towns.Select(x => x.Id).SingleOrDefaultAsync();

                EditTownViewModel editTown = new EditTownViewModel { Id = townId, Name = "ЕдитнатГрад" };

                // Act
                await townService.EditTownAsync(editTown);

                // Assert
                Assert.True(await context.Towns.CountAsync() == 1);

                string editTownNameInDatabase = await context.Towns.Select(x => x.Name).SingleOrDefaultAsync();
                Assert.Equal(editTownNameInDatabase, editTown.Name);
            }
        }

        [Fact]
        public async Task ShouldAllReturnedTownsEqualToTownsInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                TownService townService = new TownService(context);

                Town town1 = new Town { Name = "Град1" };
                Town town2 = new Town { Name = "Град2" };
                Town town3 = new Town { Name = "Град3" };
                Town town4 = new Town { Name = "Град4" };

                await context.Towns.AddRangeAsync(town1, town2, town3, town4);
                await context.SaveChangesAsync();

                await townService.DeleteTownAsync(town1.Id);
                int townsInDatabase = await context.Towns.CountAsync();

                // Act
                var result = await townService.GetAllTownsAsync(0, null);
                int returnedActiveTownsCount = result.Towns.Count();

                // Assert
                Assert.Equal(townsInDatabase - 1, returnedActiveTownsCount);
            }
        }

        [Fact]
        public async Task ShouldGetCorrectTownToEdit()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                TownService townService = new TownService(context);

                Town town = new Town { Name = "Таун" };
                await context.Towns.AddAsync(town);
                await context.SaveChangesAsync();

                int townId = town.Id;

                // Act
                var model = await townService.GetTownToEditAsync(townId);

                // Assert
                Assert.Equal(model.Name, town.Name);
                Assert.Equal(model.Id, town.Id);
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
