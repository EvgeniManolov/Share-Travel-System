namespace ShareTravelSystem.Tests
{
    using System;
    using Xunit;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using ShareTravelSystem.Web.Models;
    using ShareTravelSystem.Web.Areas.Admin.Controllers;
    using ShareTravelSystem.Services;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.ViewModels.Town;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using AutoMapper;
    using ShareTravelSystem.Web.Infrastructure.Mapping;

    public class TownControllerTest
    {
        //public TownControllerTest()
        //{
        //    TestStartup.Initialize();
        //}

        [Fact]
        public void EditShouldChangeTownDataInDatabase()
        {
            //Arrange
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
            //Arrange
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
            //Arrange
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
