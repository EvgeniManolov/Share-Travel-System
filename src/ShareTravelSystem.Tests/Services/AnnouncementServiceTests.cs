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
            UserManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task CreateAnnouncementAsync_WithCorrectData_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var announcementService = new AnnouncementService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                var model =
                    new CreateAnnouncementViewModel {Title = "Заглавие", Content = "Съдържание"};

                // Act
                await announcementService.CreateAnnouncementAsync(model, user.Id);

                // Assert
                Assert.True(await context.Announcements.CountAsync() == 1);

                var dbAnnouncementAuthor =
                    await context.Announcements.Select(x => x.Author.UserName).SingleOrDefaultAsync();
                Assert.Equal(user.UserName, dbAnnouncementAuthor);
            }
        }

        [Fact]
        public async Task GetIndexAnnouncementsAsync_WithCorrectData_ReturnsFourAnnouncementIfExistsMore()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var announcementService = new AnnouncementService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                var announcements = new List<CreateAnnouncementViewModel>
                {
                    new CreateAnnouncementViewModel {Title = "Заглавие1", Content = "Съдържание1"},
                    new CreateAnnouncementViewModel {Title = "Заглавие2", Content = "Съдържание2"},
                    new CreateAnnouncementViewModel {Title = "Заглавие3", Content = "Съдържание3"},
                    new CreateAnnouncementViewModel {Title = "Заглавие4", Content = "Съдържание4"},
                    new CreateAnnouncementViewModel {Title = "Заглавие5", Content = "Съдържание5"}
                };

                // Act
                await announcementService.CreateAnnouncementAsync(announcements[0], user.Id);
                await announcementService.CreateAnnouncementAsync(announcements[1], user.Id);
                await announcementService.CreateAnnouncementAsync(announcements[2], user.Id);
                await announcementService.CreateAnnouncementAsync(announcements[3], user.Id);
                await announcementService.CreateAnnouncementAsync(announcements[4], user.Id);
                var returnedModel = await announcementService.GetIndexAnnouncementsAsync();

                // Assert
                Assert.True(await context.Announcements.CountAsync() == 5);
                Assert.Equal(4, returnedModel.Count());
            }
        }

        [Fact]
        public async Task GetAllAnnouncementsAsync_WithCorrectData_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var announcementService = new AnnouncementService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                var announcements = new List<Announcement>
                {
                    new Announcement
                    {
                        Title = "Title1",
                        Content = "Content1",
                        CreateDate = DateTime.UtcNow,
                        Author = user
                    },
                    new Announcement
                    {
                        Title = "Title2",
                        Content = "Content2",
                        CreateDate = DateTime.UtcNow,
                        Author = user
                    },
                    new Announcement
                    {
                        Title = "Title3",
                        Content = "Content3",
                        CreateDate = DateTime.UtcNow,
                        Author = user
                    },
                    new Announcement
                    {
                        Title = "Title4",
                        Content = "Content4",
                        CreateDate = DateTime.UtcNow,
                        Author = user
                    },
                    new Announcement
                    {
                        Title = "Title5",
                        Content = "Content5",
                        CreateDate = DateTime.UtcNow,
                        Author = user
                    }
                };

                await context.Announcements.AddRangeAsync(announcements);
                await context.SaveChangesAsync();

                var announcementWithTitle1Id = await context.Announcements.Where(x => x.Title == "Title1")
                    .Select(x => x.Id).SingleOrDefaultAsync();

                // Act
                await announcementService.DeleteAnnouncementAsync(announcementWithTitle1Id);
                var result = await announcementService.GetAllAnnouncementsAsync(false, null, null, 0);

                // Assert
                Assert.True(await context.Announcements.CountAsync() == 5);
                Assert.Equal(4, result.AllAnnouncements.Announcements.Count());
            }
        }

        [Fact]
        public async Task DeleteAnnouncementAsync_WithCorrectData_SetFlagToTrue()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var announcementService = new AnnouncementService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                var announcement = new Announcement
                {
                    Title = "Title",
                    Content = "Content",
                    CreateDate = DateTime.UtcNow,
                    Author = user
                };

                await context.Users.AddAsync(user);
                await context.Announcements.AddAsync(announcement);
                await context.SaveChangesAsync();


                // Act
                await announcementService.DeleteAnnouncementAsync(announcement.Id);

                // Assert
                Assert.True(await context.Announcements.CountAsync() == 1);

                var flag = await context.Announcements.Select(x => x.IsDeleted).SingleOrDefaultAsync();
                Assert.True(flag);
            }
        }

        [Fact]
        public async Task DeleteAnnouncementAsync_WithWrongId_ReturnsAndCatchException()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var announcementService = new AnnouncementService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                var announcement = new Announcement
                {
                    Title = "Title",
                    Content = "Content",
                    CreateDate = DateTime.UtcNow,
                    Author = user
                };

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
        public async Task DetailsAnnouncementAsync_WithCorrectData_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var announcementService = new AnnouncementService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                var announcement = new Announcement
                {
                    Title = "Title",
                    Content = "Content",
                    CreateDate = DateTime.UtcNow,
                    Author = user
                };

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
        public async Task DetailsAnnouncementAsync_WithWrongId_ReturnsAndCatchException()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var announcementService = new AnnouncementService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                var announcement = new Announcement
                {
                    Title = "Title",
                    Content = "Content",
                    CreateDate = DateTime.UtcNow,
                    Author = user
                };

                await context.Users.AddAsync(user);
                await context.Announcements.AddAsync(announcement);
                await context.SaveChangesAsync();

                // Act
                string result = null;
                try
                {
                    var returnedModel = await announcementService.DetailsAnnouncementAsync(3);
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
        public async Task GetAnnouncementToEditAsync_WithCorrectData_ReturnsCorrectAnnouncement()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var announcementService = new AnnouncementService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                var announcement = new Announcement
                {
                    Title = "Title",
                    Content = "Content",
                    CreateDate = DateTime.UtcNow,
                    Author = user
                };

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
        public async Task GetAnnouncementToEditAsync_WithWrongAnnouncementId_ReturnsAndCatchException()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var announcementService = new AnnouncementService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                var announcement = new Announcement
                {
                    Title = "Title",
                    Content = "Content",
                    CreateDate = DateTime.UtcNow,
                    Author = user
                };

                await context.Users.AddAsync(user);
                await context.Announcements.AddAsync(announcement);
                await context.SaveChangesAsync();

                //Act
                string result = null;
                try
                {
                    var returnedModel = await announcementService.GetAnnouncementToEditAsync(3, user.Id);
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
        public async Task GetAnnouncementToEditAsync_WithWrongUserId_ReturnsAndCatchException()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var announcementService = new AnnouncementService(context, UserManager);

                var users = new List<ShareTravelSystemUser>
                {
                    new ShareTravelSystemUser {UserName = "TestUser"},
                    new ShareTravelSystemUser {UserName = "TestUser2"}
                };

                var announcement = new Announcement
                {
                    Title = "Title",
                    Content = "Content",
                    CreateDate = DateTime.UtcNow,
                    Author = users[0]
                };

                await context.Users.AddRangeAsync(users);
                await context.Announcements.AddAsync(announcement);
                await context.SaveChangesAsync();

                //Act
                string result = null;
                try
                {
                    var returnedModel =
                        await announcementService.GetAnnouncementToEditAsync(announcement.Id, users[1].Id);
                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                // Assert
                Assert.Equal("User with id: " + users[1].Id + " is not authorized for this operation.", result);
            }
        }

        [Fact]
        public async Task EditAnnouncementAsync_WithCorrectData_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                var announcementService = new AnnouncementService(context, UserManager);

                var user = new ShareTravelSystemUser
                {
                    UserName = "TestUser"
                };

                var announcement = new Announcement
                {
                    Title = "Title",
                    Content = "Content",
                    CreateDate = DateTime.UtcNow,
                    Author = user
                };

                await context.Users.AddAsync(user);
                await context.Announcements.AddAsync(announcement);
                await context.SaveChangesAsync();

                var model = new EditAnnouncementViewModel
                {
                    Id = announcement.Id,
                    Title = "EditedTitle",
                    Content = "EditedContent"
                };

                // Act
                await announcementService.EditAnnouncementAsync(model);

                // Assert
                var dbAnnouncementContent = await context.Announcements.Where(x => x.Id == announcement.Id)
                    .Select(x => x.Content).SingleOrDefaultAsync();
                var dbAnnouncementTitle = await context.Announcements.Where(x => x.Id == announcement.Id)
                    .Select(x => x.Title).SingleOrDefaultAsync();
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