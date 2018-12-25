namespace ShareTravelSystem.Services
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Town;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class TownService : ITownService
    {
        private readonly ShareTravelSystemDbContext db;

        private readonly UserManager<ShareTravelSystemUser> userManager;

        public TownService(ShareTravelSystemDbContext db, UserManager<ShareTravelSystemUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public void Create(CrateTownViewModel model)
        {
            Town town = new Town { Name = model.Name };
            db.Towns.Add(town);
            db.SaveChanges();
        }

        public TownPaginationViewModel GetAllTowns(int size, int page)
        {

            var towns = this.db.Towns.ProjectTo<DisplayTownViewModel>().ToList();
            var count = towns.Count();
            towns = towns.Skip((page - 1) * size).Take(size).ToList();
            return new TownPaginationViewModel { Size = size, Page = page, Count = count, Towns = towns };
        }

        public EditTownViewModel GetTownById(int id)
        {
            return this.db.Towns.Where(t => t.Id == id).Select(t => new EditTownViewModel
            {
                Id = t.Id,
                Name = t.Name
            }).FirstOrDefault();
        }


    }
}
