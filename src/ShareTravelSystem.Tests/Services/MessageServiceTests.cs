namespace ShareTravelSystem.Tests.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ShareTravelSystem.Services;
    using Web.Areas.Identity.Data;
    using Web.Models;
    using Xunit;

    public class MessageServiceTests
    {
        private UserManager<ShareTravelSystemUser> UserManager { get; set; }

        public MessageServiceTests()
        {
            UserManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task CreateMessageAsync_WithCorrectData_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                MessageService messageService = new MessageService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();



                // Act
                await messageService.CreateMessageAsync("Съобщение", user.Id);

                // Assert
                Assert.True(await context.Messages.CountAsync() == 1);
                Assert.NotNull(await context.Messages.Where(x => x.Author == user && x.Text == "Съобщение").SingleOrDefaultAsync());
            }
        }

        [Fact]
        public async Task GetAllMessagesAsync_WithCorrectData_ReturnsAllActiveMessages()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                MessageService messageService = new MessageService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                await messageService.CreateMessageAsync("Съобщение1", user.Id);
                await messageService.CreateMessageAsync("Съобщение2", user.Id);
                await messageService.CreateMessageAsync("Съобщение3", user.Id);


                // Act
                var result = await messageService.GetAllMessagesAsync(null, 0);

                // Assert
                Assert.Equal(3, result.Messages.Messages.Count());
                Assert.NotNull(result);
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
