namespace ShareTravelSystem.Tests.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ShareTravelSystem.Services;
    using ViewModels.Town;
    using Web.Areas.Identity.Data;
    using Web.Models;
    using Xunit;

    public class TownServiceTests
    {
        private UserManager<ShareTravelSystemUser> UserManager { get; set; }

        public TownServiceTests()
        {
            UserManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task CreateTownAsync_WithCorrectData_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var townService = new TownService(context);

                var model = new CrateTownViewModel {Name = "ИмеГрад"};

                // Act
                await townService.CreateTownAsync(model);

                // Assert
                Assert.True(await context.Towns.CountAsync() == 1);

                var dbTownName = await context.Towns.Select(x => x.Name).SingleOrDefaultAsync();
                var modelTownName = model.Name;

                Assert.Equal(dbTownName, modelTownName);
            }
        }

        [Fact]
        public async Task CreateTownAsync_WithAlreadyExistsTownName_ReturnsAndCatchException()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var townService = new TownService(context);

                var model = new CrateTownViewModel {Name = "ИмеГрад"};
                await townService.CreateTownAsync(model);
                var model1 = new CrateTownViewModel {Name = "ИмеГрад" };

                // Act
                string result = null;
                try
                {
                    await townService.CreateTownAsync(model1);
                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                // Assert
                Assert.Equal("Town with name ИмеГрад already exists.", result);
            }
        }

        [Fact]
        public async Task DeleteTownAsync_WithCorrectData_SetFlagDeleteToTrue()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var townService = new TownService(context);

                var town = new Town {Name = "ИзтритГрад"};
                await context.Towns.AddAsync(town);
                await context.SaveChangesAsync();

                var townId = await context.Towns.Select(x => x.Id).SingleOrDefaultAsync();

                // Act
                await townService.DeleteTownAsync(townId);

                // Assert
                Assert.True(await context.Towns.Where(x => x.IsDeleted).CountAsync() == 1);
                var flag = await context.Towns.Select(x => x.IsDeleted).SingleOrDefaultAsync();
                Assert.True(flag);
            }
        }

        [Fact]
        public async Task DeleteTownAsync_WithInCorrectData_ReturnsAndCatchException()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var townService = new TownService(context);

                var town = new Town {Name = "ИзтритГрад"};
                await context.Towns.AddAsync(town);
                await context.SaveChangesAsync();

                var townId = await context.Towns.Select(x => x.Id).SingleOrDefaultAsync();

                // Act
                string result = null;
                try
                {
                    await townService.DeleteTownAsync(3);
                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                // Assert
                Assert.Equal("Town with id: 3 does not exist.", result);
            }
        }

        [Fact]
        public async Task EditTownAsync_WithCorrectModel_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var townService = new TownService(context);

                var town = new Town {Name = "ИзтритГрад"};
                await context.Towns.AddAsync(town);
                await context.SaveChangesAsync();

                var townId = await context.Towns.Select(x => x.Id).SingleOrDefaultAsync();

                var editTown = new EditTownViewModel {Id = townId, Name = "ЕдитнатГрад"};

                // Act
                await townService.EditTownAsync(editTown);

                // Assert
                Assert.True(await context.Towns.CountAsync() == 1);

                var editTownNameInDatabase = await context.Towns.Select(x => x.Name).SingleOrDefaultAsync();
                Assert.Equal(editTownNameInDatabase, editTown.Name);
            }
        }

        [Fact]
        public async Task EditTownAsync_WithTownNameThatAlreadyExist_ReturnsAndCatchException()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var townService = new TownService(context);

                var town = new Town {Name = "ИзтритГрад"};
                await context.Towns.AddAsync(town);
                await context.SaveChangesAsync();

                var townId = await context.Towns.Select(x => x.Id).SingleOrDefaultAsync();

                var editTown = new EditTownViewModel {Id = townId, Name = "ИзтритГрад"};

                // Act
                string result = null;
                try
                {
                    await townService.EditTownAsync(editTown);
                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                // Assert
                Assert.Equal("Town with name ИзтритГрад already exists.", result);
            }
        }

        [Fact]
        public async Task GetAllTownsAsync_WithCorrectData_ReturnsAllActiveTowns()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var townService = new TownService(context);

                var town1 = new Town {Name = "Град1"};
                var town2 = new Town {Name = "Град2"};
                var town3 = new Town {Name = "Град3"};
                var town4 = new Town {Name = "Град4"};

                await context.Towns.AddRangeAsync(town1, town2, town3, town4);
                await context.SaveChangesAsync();

                await townService.DeleteTownAsync(town1.Id);
                var townsInDatabase = await context.Towns.CountAsync();

                // Act
                var result = await townService.GetAllTownsAsync(0, null);
                var returnedActiveTownsCount = result.Towns.Count();

                // Assert
                Assert.Equal(townsInDatabase - 1, returnedActiveTownsCount);
            }
        }

        [Fact]
        public async Task GetTownToEditAsync_WithCorrectId_ReturnCorrectTownToEdit()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var townService = new TownService(context);

                var town = new Town {Name = "Таун"};
                await context.Towns.AddAsync(town);
                await context.SaveChangesAsync();

                var townId = town.Id;

                // Act
                var model = await townService.GetTownToEditAsync(townId);

                // Assert
                Assert.Equal(model.Name, town.Name);
                Assert.Equal(model.Id, town.Id);
            }
        }

        [Fact]
        public async Task GetTownToEditAsync_WithInCorrectId_ReturnsAndCatchException()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var townService = new TownService(context);

                var town = new Town {Name = "Таун"};
                await context.Towns.AddAsync(town);
                await context.SaveChangesAsync();

                var townId = town.Id;

                // Act
                string result = null;
                try
                {
                    var returnedModel = await townService.GetTownToEditAsync(3);
                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                // Assert
                Assert.Equal("Town with id: 3 does not exist.", result);
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