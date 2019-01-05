using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShareTravelSystem.Data.Models;
using ShareTravelSystem.Services;
using ShareTravelSystem.ViewModels;
using ShareTravelSystem.ViewModels.Town;
using ShareTravelSystem.Web.Areas.Admin.Controllers;
using ShareTravelSystem.Web.Areas.Identity.Data;
using ShareTravelSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ShareTravelSystem.Tests.Controllers
{
    public class TownControllerTest
    {

        //private UserManager<ShareTravelSystemUser> userManager { get; set; }
        //public TownControllerTest()
        //{
        //    //TestStartup.Initialize();
        //    userManager= TestStartup.UserManager;
        //}

        [Fact]
        public void EditShouldChangeTownDataInDatabase()
        {
            ShareTravelSystemDbContext db = this.GetContext();
            TownController controller = this.GetController(db);

            int townId = 1;

            db.Towns.Add(new Town
            {
                Id = townId,
                Name = "гр.Някой"
            });

            db.SaveChanges();

            EditTownViewModel updatedTown = new EditTownViewModel
            {
                Id = 1,
                Name = "Никой"
            };

            controller.Edit(updatedTown);
            db.Towns.Where(t => t.Id == townId).Select(x => x.Name).SingleOrDefault().Should().Equals("Никой");
        }

        [Fact]
        public void DeleteShouldSetFlagToTownDataInDatabase()
        {
            ShareTravelSystemDbContext db = this.GetContext();
            TownController controller = this.GetController(db);

            int townId = 1;
            Town town = new Town
            {
                Id = townId,
                Name = "гр.Изтрит"
            };

            db.Towns.Add(town);
            db.SaveChanges();

            controller.Delete(town.Id);
            db.Towns.Where(t => t.Id == townId).Select(x => x.IsDeleted).SingleOrDefault().Should().Equals("true");
        }

        [Fact]
        public void CreateShouldCreateTownDataInDatabase()
        {
          

            ShareTravelSystemDbContext db = this.GetContext();
            TownController controller = this.GetController(db);

            CrateTownViewModel town = new CrateTownViewModel
            {
                Name = "гр.Създавам"
            };

            controller.Create(town);
            db.Towns.Where(t => t.Name == "гр.Създавам").Select(x => x.Name).SingleOrDefault().Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void IndexShouldReturnViewResultWithTownPaginationViewModel()
        {
            var db = this.GetContext();

            var controller = this.GetController(db);

            var result = controller.Index(1, null);

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().Model.Should().BeOfType<TownPaginationViewModel>();

        }

        private TownController GetController(ShareTravelSystemDbContext db)
        {

            TownService towns = new TownService(db);

            TownController controller = new TownController(towns);

            return controller;
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
