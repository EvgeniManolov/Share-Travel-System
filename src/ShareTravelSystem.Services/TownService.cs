using Microsoft.AspNetCore.Identity;
using ShareTravelSystem.Data.Models;
using ShareTravelSystem.Services.Contracts;
using ShareTravelSystem.Web.Areas.Identity.Data;
using ShareTravelSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareTravelSystem.Services
{
    public class TownService : ITownService
    {
        private readonly ShareTravelSystemDbContext db;

        private readonly UserManager<ShareTravelSystemUser> userManager;

        public TownService(ShareTravelSystemDbContext db, UserManager<ShareTravelSystemUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public List<Town> GetAllTowns()
        {
            return this.db.Towns.ToList();
        }

    }
}
