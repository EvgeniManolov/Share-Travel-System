﻿namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels.Offer;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using System.Collections.Generic;

    public class OfferController : Controller
    {

        private readonly IOfferService offerService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public OfferController(IOfferService offerService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.offerService = offerService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            List<Town> towns = this.offerService.GetAllTowns();
            ViewData["Towns"] = towns;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateOfferViewModel model, string returnUrl = null)
        {
            string currentUserId = this.userManager.GetUserId(this.User);
            this.offerService.Create(model, currentUserId);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
