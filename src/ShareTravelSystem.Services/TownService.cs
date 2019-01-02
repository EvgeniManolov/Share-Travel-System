namespace ShareTravelSystem.Services
{
    using AutoMapper.QueryableExtensions;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Services.Infrastructure;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Town;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TownService : ITownService
    {
        private readonly ShareTravelSystemDbContext db;

        public TownService(ShareTravelSystemDbContext db)
        {
            this.db = db;
        }

        public void Create(CrateTownViewModel model)
        {
            string isExist = this.db.Towns.Where(t => t.Name.ToLower() == model.Name.ToLower()).Select(x => x.Name).SingleOrDefault();

            if (isExist != null)
            {
                throw new ArgumentException(string.Format(Constants.TownAlreadyExists, model.Name));
            }

            Town town = new Town { Name = model.Name };

            db.Towns.Add(town);
            db.SaveChanges();
        }

        public void Delete(int townId)
        {
            Town town = this.db
                .Towns
                .Where(t => t.Id == townId)
                .SingleOrDefault();

            if (town == null)
            {
                throw new ArgumentException(string.Format(Constants.TownDoesNotExist, townId));
            }

            town.IsDeleted = true;
            this.db.SaveChanges();
        }

        public void EditTownById(EditTownViewModel model)
        {
            Town town = this.db
                .Towns
                .Where(t => t.Id == model.Id)
                .SingleOrDefault();

            string isExist = this.db.Towns.Where(t => t.Name == model.Name).Select(n => n.Name).SingleOrDefault();
            if (isExist != null)
            {
                throw new ArgumentException(string.Format(Constants.TownAlreadyExists, model.Name));
            }

            town.Name = model.Name;
            this.db.SaveChanges();
        }

        public TownPaginationViewModel GetAllTowns(int page, string search)
        {

            int size = Constants.TownsPerPage;
            if (page == 0) page = 1;
            List<DisplayTownViewModel> towns = new List<DisplayTownViewModel>();

            if (search != null && search != "")
            {
                towns = this.db
                            .Towns
                            .OrderBy(p => p.Name)
                            .Where(t => t.IsDeleted == false && t.Name.ToLower()
                            .Contains(search.ToLower()))
                            .ProjectTo<DisplayTownViewModel>()
                            .ToList();
            }
            else
            {
                towns = this.db
                            .Towns
                            .OrderBy(p => p.Name)
                            .Where(t => t.IsDeleted == false)
                            .ProjectTo<DisplayTownViewModel>()
                            .ToList();
            }

            int count = towns.Count();
            towns = towns.Skip((page - 1) * size).Take(size).ToList();

            TownPaginationViewModel result = new TownPaginationViewModel
            {
                Size = size,
                Page = page,
                Count = count,
                Towns = towns,
                Search = search
            };

            return result;
        }

        public EditTownViewModel GetTownById(int id)
        {
            EditTownViewModel town = this.db
                .Towns
                .Where(t => t.Id == id)
                .Select(t => new EditTownViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).FirstOrDefault();

            if (town == null)
            {
                throw new ArgumentException(string.Format(Constants.TownDoesNotExist, id));
            }

            return town;
        }
    }
}
