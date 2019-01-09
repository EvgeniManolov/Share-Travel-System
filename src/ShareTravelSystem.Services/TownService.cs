namespace ShareTravelSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Data.Models;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;
    using ViewModels.Pagination;
    using ViewModels.Town;

    public class TownService : ITownService
    {
        private readonly ShareTravelSystemDbContext db;

        public TownService(ShareTravelSystemDbContext db)
        {
            this.db = db;
        }

        public async Task CreateTownAsync(CrateTownViewModel model)
        {
            var isExist = await this.db.Towns
                .Where(t => String.Equals(t.Name, model.Name, StringComparison.CurrentCultureIgnoreCase) && !t.IsDeleted)
                .Select(x => x.Name).SingleOrDefaultAsync();

            if (isExist != null)
            {
                throw new ArgumentException(string.Format(Constants.TownAlreadyExists, model.Name));
            }

            var town = new Town { Name = model.Name };

            await this.db.Towns.AddAsync(town);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteTownAsync(int townId)
        {
            var town = await this.db
                .Towns
                .Where(t => t.Id == townId)
                .SingleOrDefaultAsync();

            if (town == null)
            {
                throw new ArgumentException(string.Format(Constants.TownDoesNotExist, townId));
            }

            town.IsDeleted = true;
            await this.db.SaveChangesAsync();
        }

        public async Task EditTownAsync(EditTownViewModel model)
        {
            var townTask = this.db
                .Towns
                .Where(t => t.Id == model.Id && !t.IsDeleted)
                .SingleOrDefaultAsync();

            var isExist = await this.db.Towns.Where(t => t.Name == model.Name && !t.IsDeleted).Select(n => n.Name)
                .SingleOrDefaultAsync();
            if (isExist != null)
            {
                throw new ArgumentException(string.Format(Constants.TownAlreadyExists, model.Name));
            }

            var town = await townTask;
            town.Name = model.Name;
            await this.db.SaveChangesAsync();
        }

        public async Task<TownPaginationViewModel> GetAllTownsAsync(int page, string search)
        {
            var size = Constants.TownsPerPage;
            if (page == 0) page = 1;
            List<DisplayTownViewModel> towns;

            if (!string.IsNullOrEmpty(search))
            {
                towns = await this.db
                    .Towns
                    .OrderBy(p => p.Name)
                    .Where(t => !t.IsDeleted && t.Name.ToLower()
                                    .Contains(search.ToLower()))
                    .ProjectTo<DisplayTownViewModel>()
                    .ToListAsync();
            }
            else
            {
                towns = await this.db
                    .Towns
                    .OrderBy(p => p.Name)
                    .Where(t => !t.IsDeleted)
                    .ProjectTo<DisplayTownViewModel>()
                    .ToListAsync();
            }

            var count = towns.Count();
            towns = towns.Skip((page - 1) * size).Take(size).ToList();

            var result = new TownPaginationViewModel
            {
                Size = size,
                Page = page,
                Count = count,
                Towns = towns,
                Search = search
            };

            return result;
        }

        public async Task<EditTownViewModel> GetTownToEditAsync(int id)
        {
            var town = await this.db
                .Towns
                .Where(t => t.Id == id && !t.IsDeleted)
                .ProjectTo<EditTownViewModel>()
                .SingleOrDefaultAsync();

            if (town == null)
            {
                throw new ArgumentException(string.Format(Constants.TownDoesNotExist, id));
            }

            return town;
        }
    }
}