﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShareTravelSystem.Data.Models;
using ShareTravelSystem.Services.Contracts;
using ShareTravelSystem.ViewModels;
using ShareTravelSystem.ViewModels.Town;
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
        public IActionResult All(int page)
        {
            var asd = HttpContext.Request;
            if (page == 0) page = 1;
            int size = 10;
            TownPaginationViewModel towns = this.townService.GetAllTowns(size, page);

            return View(towns);
        }

        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CrateTownViewModel model, string returnUrl = null)
        {
            this.townService.Create(model);

            return RedirectToAction(nameof(TownController.All), "Town");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var town = this.townService.GetTownById(id);
            return View(town);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

            return View();
        }
    }
}
