namespace ShareTravelSystem.Tests.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AnnouncementServiceTests
    {
        private UserManager<ShareTravelSystemUser> userManager { get; set; }

        public AnnouncementServiceTests()
        {
            userManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task ShouldCreateAnnouncementAndSeeDataInDatabase()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            AnnouncementService announcementService = new AnnouncementService(db, userManager);

            var user = new ShareTravelSystemUser
            {
                UserName = "TestUser"
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            CreateAnnouncementViewModel model = new CreateAnnouncementViewModel { Title = "Заглавие", Content = "Съдържание" };

            // Act
            await announcementService.CreateAnnouncementAsync(model, user.Id);

            // Assert
            Assert.True(await db.Announcements.CountAsync() == 1);

            string dbAnnouncementAuthor = await db.Announcements.Select(x => x.Author.UserName).SingleOrDefaultAsync();
            Assert.Equal(user.UserName, dbAnnouncementAuthor);
        }

        [Fact]
        public async Task ShouldReturnedFourAnnouncementsForIndexPageIfExistsMoreInDatabase()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            AnnouncementService announcementService = new AnnouncementService(db, userManager);

            var user = new ShareTravelSystemUser
            {
                UserName = "TestUser"
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

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
            Assert.True(await db.Announcements.CountAsync() == 5);

            Assert.Equal(4, returnedModel.Count());
        }

        [Fact]
        public async Task ShouldReturnedAllActiveAnnouncementsInDatabase()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            AnnouncementService announcementService = new AnnouncementService(db, userManager);

            var user = new ShareTravelSystemUser
            {
                UserName = "TestUser"
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            List<Announcement> announcements = new List<Announcement>()
                { new Announcement{ Title = "Title1", Content="Content1",                           CreateDate=DateTime.UtcNow, Author=user},
                 new Announcement{ Title = "Title2", Content="Content2",                           CreateDate=DateTime.UtcNow, Author=user},
                 new Announcement{ Title = "Title3", Content="Content3",                           CreateDate=DateTime.UtcNow, Author=user},
                 new Announcement{ Title = "Title4", Content="Content4",                           CreateDate=DateTime.UtcNow, Author=user},
                 new Announcement{ Title = "Title5", Content="Content5",                           CreateDate=DateTime.UtcNow, Author=user},
        };

            await db.Announcements.AddRangeAsync(announcements);
            await db.SaveChangesAsync();

            int announcementWithTitle1Id = await db.Announcements.Where(x => x.Title == "Title1").Select(x => x.Id).SingleOrDefaultAsync();

            // Act
            await announcementService.DeleteAnnouncementAsync(announcementWithTitle1Id);
            var result = await announcementService.GetAllAnnouncementsAsync(false, null, null, 0);

            // Assert
            Assert.True(await db.Announcements.CountAsync() == 5);

            Assert.Equal(4, result.AllAnnouncements.Announcements.Count());
        }

        [Fact]
        public async Task ShouldDeleteAnnouncementAndSeeSetFlagToTrueInDatabase()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            AnnouncementService announcementService = new AnnouncementService(db, userManager);

            var user = new ShareTravelSystemUser
            {
                UserName = "TestUser"
            };

            Announcement announcement = new Announcement { Title = "Title", Content = "Content", CreateDate = DateTime.UtcNow, Author = user };

            await db.Users.AddAsync(user);
            await db.Announcements.AddAsync(announcement);
            await db.SaveChangesAsync();


            // Act
            await announcementService.DeleteAnnouncementAsync(announcement.Id);

            // Assert
            Assert.True(await db.Announcements.CountAsync() == 1);

            bool flag = await db.Announcements.Select(x => x.IsDeleted).SingleOrDefaultAsync();
            bool variableTrue = true;
            Assert.Equal(variableTrue, flag);
        }

        [Fact]
        public async Task ShouldDetailsAnnouncementDisplayDataAsThisInDatabase()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            AnnouncementService announcementService = new AnnouncementService(db, userManager);

            var user = new ShareTravelSystemUser
            {
                UserName = "TestUser"
            };

            Announcement announcement = new Announcement { Title = "Title", Content = "Content", CreateDate = DateTime.UtcNow, Author = user };

            await db.Users.AddAsync(user);
            await db.Announcements.AddAsync(announcement);
            await db.SaveChangesAsync();


            // Act
            var returnedModel = await announcementService.DetailsAnnouncementAsync(announcement.Id);

            // Assert
            Assert.Equal(announcement.Id, returnedModel.Id);
            Assert.Equal(announcement.Content, returnedModel.Content);
            Assert.Equal(announcement.Title, returnedModel.Title);
            Assert.Equal(announcement.CreateDate, returnedModel.CreateDate);
            Assert.Equal(announcement.Author.UserName, returnedModel.Author);
        }

        [Fact]
        public async Task ShouldGetCorrectAnnouncementToEdit()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            AnnouncementService announcementService = new AnnouncementService(db, userManager);

            var user = new ShareTravelSystemUser
            {
                UserName = "TestUser"
            };

            Announcement announcement = new Announcement { Title = "Title", Content = "Content", CreateDate = DateTime.UtcNow, Author = user };

            await db.Users.AddAsync(user);
            await db.Announcements.AddAsync(announcement);
            await db.SaveChangesAsync();


            // Act
            var returnedModel = await announcementService.GetAnnouncementToEditAsync(announcement.Id, user.Id);

            // Assert
            Assert.Equal(announcement.Id, returnedModel.Id);
            Assert.Equal(announcement.Content, returnedModel.Content);
            Assert.Equal(announcement.Title, returnedModel.Title);
        }


        [Fact]
        public async Task ShouldEditAnnouncementAndSeeEditedDataInDatabase()
        {
            // Arrange
            ShareTravelSystemDbContext db = this.GetContext();
            AnnouncementService announcementService = new AnnouncementService(db, userManager);

            var user = new ShareTravelSystemUser
            {
                UserName = "TestUser"
            };

            Announcement announcement = new Announcement { Title = "Title", Content = "Content", CreateDate = DateTime.UtcNow, Author = user };

            await db.Users.AddAsync(user);
            await db.Announcements.AddAsync(announcement);
            await db.SaveChangesAsync();

            EditAnnouncementViewModel model = new EditAnnouncementViewModel
            {
                Id = announcement.Id,
                Title = "EditedTitle",
                Content = "EditedContent"
            };

            // Act
            await announcementService.EditAnnouncementAsync(model);

            // Assert
            string dbAnnouncementContent = await db.Announcements.Where(x => x.Id == announcement.Id).Select(x => x.Content).SingleOrDefaultAsync();
            string dbAnnouncementTitle = await db.Announcements.Where(x => x.Id == announcement.Id).Select(x => x.Title).SingleOrDefaultAsync();
            Assert.Equal(model.Content, dbAnnouncementContent);
            Assert.Equal(model.Title, dbAnnouncementTitle);
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
