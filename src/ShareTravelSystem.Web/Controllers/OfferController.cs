namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Offer;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using System.Collections.Generic;
    using System.Linq;

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
            List<Town> towns = this.offerService.GetAllTowns().ToList();
            ViewData["Towns"] = towns;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateOfferViewModel model, string returnUrl = null)
        {
            string currentUserId = this.userManager.GetUserId(this.User);
            this.offerService.Create(model, currentUserId);

            return RedirectToAction(nameof(OfferController.All), "Offer");
        }

        [HttpGet]
        public IActionResult All(string filter, string search, bool privateOffers, int page)
        {
            if (page == 0) page = 1;
            int size = 9;
            string currentUserId = this.userManager.GetUserId(this.User);
            OfferPaginationViewModel result = this.offerService.GetAllOffers(privateOffers, filter, search, currentUserId, page, size);

            ViewData["Title"] = result.TitleOfPage;

            return this.View(result);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            DetailsOfferViewModel model = this.offerService.GetOfferById(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            DisplayEditOfferViewModel model = this.offerService.GetOfferToEdit(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DisplayEditOfferViewModel model)
        {
            this.offerService.EditOffer(model);
             return this.Redirect("/offer/details?id=" + model.OfferModel.Id);
        }
    }
}
