using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShareTravelSystem.Services;
using ShareTravelSystem.Web.Areas.Identity.Data;
using ShareTravelSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShareTravelSystem.Tests.Services
{
    public class MessageServiceTests
    {
        private UserManager<ShareTravelSystemUser> userManager { get; set; }

        public MessageServiceTests()
        {
            userManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task ShouldCreateMessageAndSeeDataInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                MessageService messageService = new MessageService(context, userManager);

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
        public async Task ShouldGetAllMessagesFromDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                MessageService messageService = new MessageService(context, userManager);

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
                var result = await messageService.GetAllMessagesAsync();

                // Assert
                Assert.Equal(3, result.Messages.Count());
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
