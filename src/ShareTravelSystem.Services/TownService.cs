namespace ShareTravelSystem.Services
{
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
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

        public List<Town> GetAllTowns()
        {
            return this.db.Towns.ToList();
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
