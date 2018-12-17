using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShareTravelSystem.Data.Models;
using ShareTravelSystem.Services.Contracts;
using ShareTravelSystem.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareTravelSystem.Web.Controllers
{
    public class TownController : Controller
    {
        private readonly ITownService townService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public TownController(ITownService townService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.townService = townService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult All(string returnUrl = null)
        {
            List<Town> towns = this.townService.GetAllTowns();

            return View(towns);
        }
    }
}
