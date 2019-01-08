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
    using ViewModels;
    using Web.Areas.Identity.Data;
    using Web.Models;
    using Xunit;

    public class AnnouncementServiceTests
    {
        private UserManager<ShareTravelSystemUser> UserManager { get; set; }

        public AnnouncementServiceTests()
        {
            this.UserManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task ShouldCreateAnnouncementAndSeeDataInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                AnnouncementService announcementService = new AnnouncementService(context, this.UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                CreateAnnouncementViewModel model = new CreateAnnouncementViewModel { Title = "Заглавие", Content = "Съдържание" };

                // Act
                await announcementService.CreateAnnouncementAsync(model, user.Id);

                // Assert
                Assert.True(await context.Announcements.CountAsync() == 1);

                string dbAnnouncementAuthor = await context.Announcements.Select(x => x.Author.UserName).SingleOrDefaultAsync();
                Assert.Equal(user.UserName, dbAnnouncementAuthor);
            }
        }

        [Fact]
        public async Task ShouldReturnedFourAnnouncementsForIndexPageIfExistsMoreInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                AnnouncementService announcementService = new AnnouncementService(context, this.UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                CreateAnnouncementViewModel model1 = new CreateAnnouncementViewModel { Title = "Заглавие1", Content = "Съдържание1" };
                CreateAnnouncementViewModel model2 = new CreateAnnouncementViewModel { Title = "Заглавие2", Content = "Съдържание2" };
                CreateAnnouncementViewModel model3 = new CreateAnnouncementViewModel { Title = "Заглавие3", Content = "Съдържание3" };
                CreateAnnouncementViewModel model4 = new CreateAnnouncementViewModel { Title = "Заглавие4", Content = "Съдържание4" };
                CreateAnnouncementViewModel model5 = new CreateAnnouncementViewModel { Title = "Заглавие5", Content = "Съдържание5" };

                // Act
                await announcementService.CreateAnnouncementAsync(model1, user.Id);
                await announcementService.CreateAnnouncementAsync(model2, user.Id);
                await announcementService.CreateAnnouncementAsync(model3, user.Id);
                await announcementService.CreateAnnouncementAsync(model4, user.Id);
                await announcementService.CreateAnnouncementAsync(model5, user.Id);
                var returnedModel = await announcementService.GetIndexAnnouncementsAsync();

                // Assert
                Assert.True(await context.Announcements.CountAsync() == 5);
                Assert.Equal(4, returnedModel.Count());
            }
        }

        [Fact]
        public async Task ShouldReturnedAllActiveAnnouncementsInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                AnnouncementService announcementService = new AnnouncementService(context, this.UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                List<Announcement> announcements = new List<Announcement>
                { new Announcement{ Title = "Title1", Content="Content1", CreateDate=DateTime.UtcNow, Author=user},
                 new Announcement{ Title = "Title2", Content="Content2", CreateDate=DateTime.UtcNow, Author=user},
                 new Announcement{ Title = "Title3", Content="Content3", CreateDate=DateTime.UtcNow, Author=user},
                 new Announcement{ Title = "Title4", Content="Content4", CreateDate=DateTime.UtcNow, Author=user},
                 new Announcement{ Title = "Title5", Content="Content5", CreateDate=DateTime.UtcNow, Author=user}
        };

                await context.Announcements.AddRangeAsync(announcements);
                await context.SaveChangesAsync();

                int announcementWithTitle1Id = await context.Announcements.Where(x => x.Title == "Title1").Select(x => x.Id).SingleOrDefaultAsync();

                // Act
                await announcementService.DeleteAnnouncementAsync(announcementWithTitle1Id);
                var result = await announcementService.GetAllAnnouncementsAsync(false, null, null, 0);

                // Assert
                Assert.True(await context.Announcements.CountAsync() == 5);
                Assert.Equal(4, result.AllAnnouncements.Announcements.Count());
            }
        }

        [Fact]
        public async Task ShouldDeleteAnnouncementAndSeeSetFlagToTrueInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                AnnouncementService announcementService = new AnnouncementService(context, this.UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                Announcement announcement = new Announcement { Title = "Title", Content = "Content", CreateDate = DateTime.UtcNow, Author = user };

                await context.Users.AddAsync(user);
                await context.Announcements.AddAsync(announcement);
                await context.SaveChangesAsync();


                // Act
                await announcementService.DeleteAnnouncementAsync(announcement.Id);

                // Assert
                Assert.True(await context.Announcements.CountAsync() == 1);

                bool flag = await context.Announcements.Select(x => x.IsDeleted).SingleOrDefaultAsync();
                Assert.True(flag);
            }
        }

        [Fact]
        public async Task ShouldDeleteAnnouncementThatDoesNotExist()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                AnnouncementService announcementService = new AnnouncementService(context, this.UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                Announcement announcement = new Announcement { Title = "Title", Content = "Content", CreateDate = DateTime.UtcNow, Author = user };

                await context.Users.AddAsync(user);
                await context.Announcements.AddAsync(announcement);
                await context.SaveChangesAsync();

                

                // Act
                string result = null;
                try
                {
                    await announcementService.DeleteAnnouncementAsync(3);
                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                // Assert
                Assert.Equal("Announcement with id: 3 does not exist.", result);
            }
        }

        [Fact]
        public async Task ShouldDetailsAnnouncementDisplayDataAsThisInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                AnnouncementService announcementService = new AnnouncementService(context, this.UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                Announcement announcement = new Announcement { Title = "Title", Content = "Content", CreateDate = DateTime.UtcNow, Author = user };

                await context.Users.AddAsync(user);
                await context.Announcements.AddAsync(announcement);
                await context.SaveChangesAsync();


                // Act
                var returnedModel = await announcementService.DetailsAnnouncementAsync(announcement.Id);

                // Assert
                Assert.Equal(announcement.Id, returnedModel.Id);
                Assert.Equal(announcement.Content, returnedModel.Content);
                Assert.Equal(announcement.Title, returnedModel.Title);
                Assert.Equal(announcement.CreateDate, returnedModel.CreateDate);
                Assert.Equal(announcement.Author.UserName, returnedModel.Author);
            }
        }

        [Fact]
        public async Task ShouldDetailsAnnouncementThatDoesNotExistAndTakeException()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                AnnouncementService announcementService = new AnnouncementService(context, this.UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                Announcement announcement = new Announcement { Title = "Title", Content = "Content", CreateDate = DateTime.UtcNow, Author = user };

                await context.Users.AddAsync(user);
                await context.Announcements.AddAsync(announcement);
                await context.SaveChangesAsync();


                // Act
                string result = null;
                try
                {
                    var returnedModel = await announcementService.DetailsAnnouncementAsync(3);
                }
                catch(Exception e)
                {
                    result = e.Message;
                }

                // Assert
                Assert.Equal("Announcement with id: 3 does not exist.", result);
            }
        }

        [Fact]
        public async Task ShouldGetCorrectAnnouncementToEdit()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                AnnouncementService announcementService = new AnnouncementService(context, this.UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                Announcement announcement = new Announcement { Title = "Title", Content = "Content", CreateDate = DateTime.UtcNow, Author = user };

                await context.Users.AddAsync(user);
                await context.Announcements.AddAsync(announcement);
                await context.SaveChangesAsync();


                // Act
                var returnedModel = await announcementService.GetAnnouncementToEditAsync(announcement.Id, user.Id);

                // Assert
                Assert.Equal(announcement.Id, returnedModel.Id);
                Assert.Equal(announcement.Content, returnedModel.Content);
                Assert.Equal(announcement.Title, returnedModel.Title);
            }
        }

        [Fact]
        public async Task ShouldEditAnnouncementAndSeeEditedDataInDatabase()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                AnnouncementService announcementService = new AnnouncementService(context, this.UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                Announcement announcement = new Announcement { Title = "Title", Content = "Content", CreateDate = DateTime.UtcNow, Author = user };

                await context.Users.AddAsync(user);
                await context.Announcements.AddAsync(announcement);
                await context.SaveChangesAsync();

                EditAnnouncementViewModel model = new EditAnnouncementViewModel
                {
                    Id = announcement.Id,
                    Title = "EditedTitle",
                    Content = "EditedContent"
                };

                // Act
                await announcementService.EditAnnouncementAsync(model);

                // Assert
                string dbAnnouncementContent = await context.Announcements.Where(x => x.Id == announcement.Id).Select(x => x.Content).SingleOrDefaultAsync();
                string dbAnnouncementTitle = await context.Announcements.Where(x => x.Id == announcement.Id).Select(x => x.Title).SingleOrDefaultAsync();
                Assert.Equal(model.Content, dbAnnouncementContent);
                Assert.Equal(model.Title, dbAnnouncementTitle);
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
