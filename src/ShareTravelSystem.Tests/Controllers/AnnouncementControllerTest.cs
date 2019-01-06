//using FluentAssertions;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using ShareTravelSystem.Data.Models;
//using ShareTravelSystem.Services;
//using ShareTravelSystem.ViewModels;
//using ShareTravelSystem.Web.Areas.Identity.Data;
//using ShareTravelSystem.Web.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Xunit;

//namespace ShareTravelSystem.Tests.Controllers
//{
//    public class AnnouncementControllerTest
//    {
//        private UserManager<ShareTravelSystemUser> userManager { get; set; }
//        public AnnouncementControllerTest()
//        {
//            userManager = TestStartup.UserManager;
//        }

//        [Fact]
//        public void EditShouldChangeAnnouncementDataInDatabase()
//        {
//            //Arrange
//            ShareTravelSystemDbContext db = this.GetContext();
//            Web.Areas.User.Controllers.AnnouncementController controller = GetController(db);

//            int announcementId = 1;

//            db.Announcements.Add(new Announcement
//            {
//                Id = announcementId,
//                Title = "EditTitle",
//                Content = "EditContent"
//            });
//            db.SaveChanges();

//            EditAnnouncementViewModel updatedAnnouncement = new EditAnnouncementViewModel
//            {
//                Id = announcementId,
//                Title = "Title",
//                Content = "Content"
//            };

//            controller.Edit(updatedAnnouncement);

//            Func<string, string, bool> isUpdatedAnnouncement = (title, content) => title == "Title" && content == "Content";

//            db.Announcements.Should().OnlyContain(b => isUpdatedAnnouncement(b.Title, b.Content));
//        }

//        [Fact]
//        public void DeleteShouldSetFlagToAnnouncementDataInDatabase()
//        {
//            //Arrange
//            ShareTravelSystemDbContext db = this.GetContext();
//            Web.Areas.Admin.Controllers.AnnouncementController adminController = this.GetAdminController(db);

//            int announcementId = 1;
//            Announcement announcement = new Announcement
//            {
//                Id = announcementId,
//                Title = "Изтрит",
//                Content = "Изтрит"
//            };

//            db.Announcements.Add(announcement);
//            db.SaveChanges();

//            adminController.Delete(announcement.Id);
//            db.Towns.Where(t => t.Id == announcementId).Select(x => x.IsDeleted).SingleOrDefault().Should().Equals("true");
//        }

//        [Fact]
//        public void CreateShouldCreateAnnouncementDataInDatabase()
//        {


//            //Arrange
//            ShareTravelSystemDbContext db = this.GetContext();
//            Web.Areas.User.Controllers.AnnouncementController controller = this.GetController(db);

//            var user = new ShareTravelSystemUser
//            {
//                UserName = "TestUser"
//            };

//            db.Users.Add(user);

//            db.SaveChanges();


//            CreateAnnouncementViewModel announcement = new CreateAnnouncementViewModel
//            {
//                Title = "Title",
//                Content = "Content"
//            };

//            controller.Create(announcement);
//            db.Announcements.Where(t => t.Title == "Title").Select(x => x.Title).SingleOrDefault().Should().NotBeNullOrEmpty();
//        }

//        //[Fact]
//        //public void IndexShouldReturnViewResultWithTownPaginationViewModel()
//        //{
//        //    var db = this.GetContext();

//        //    var controller = this.GetController(db);


//        //    var result = controller.Index(1, null);


//        //    result.Should().BeOfType<ViewResult>();
//        //    result.As<ViewResult>().Model.Should().BeOfType<TownPaginationViewModel>();

//        //}

//        private Web.Areas.User.Controllers.AnnouncementController GetController(ShareTravelSystemDbContext db)
//        {

//            AnnouncementService announcements = new AnnouncementService(db, userManager);

//            Web.Areas.User.Controllers.AnnouncementController controller = new Web.Areas.User.Controllers.AnnouncementController(announcements, userManager);

//            return controller;
//        }

//        private Web.Areas.Admin.Controllers.AnnouncementController GetAdminController(ShareTravelSystemDbContext db)
//        {
//            AnnouncementService announcements = new AnnouncementService(db, userManager);

//            Web.Areas.Admin.Controllers.AnnouncementController controller = new Web.Areas.Admin.Controllers.AnnouncementController(announcements, userManager);

//            return controller;
//        }

//        private ShareTravelSystemDbContext GetContext()
//        {
//            var dbOptions = new DbContextOptionsBuilder<ShareTravelSystemDbContext>()
//                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                    .Options;

//            return new ShareTravelSystemDbContext(dbOptions);
//        }
//    }
//}
