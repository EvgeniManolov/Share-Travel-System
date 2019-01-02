namespace ShareTravelSystem.Web.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Offer;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants = Infrastructure.Constants.Constants;

    [Area("User")]
    [Authorize(Roles = "User")]
    public class OfferController : BaseController
    {
        private readonly IOfferService offerService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public OfferController(IOfferService offerService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.offerService = offerService;
            this.userManager = userManager;
        }

        public IActionResult Index(string search, bool privateOffers, int page, string filter = Constants.FilterOfAllOffers)
        {
            int size = Constants.OffersPerPage;
            string currentUserId = this.userManager.GetUserId(this.User);
            OfferPaginationViewModel result = this.offerService.GetAllOffers(privateOffers, filter, search, currentUserId, page, size);
            List<int> likedOffersIds = this.offerService.GetLikedOrDislikedOffersIds(currentUserId);

            ViewData["LikedDislikedOffersIds"] = likedOffersIds;
            ViewData["Title"] = result.TitleOfPage;
            return this.View(result);
        }

       
        public IActionResult Create()
        {
            List<Town> towns = this.offerService.GetAllTowns().ToList();
            ViewData["Towns"] = towns;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            string currentUserId = this.userManager.GetUserId(this.User);
            this.offerService.Create(model, currentUserId);
            return RedirectToAction(nameof(OfferController.Index));
        }

        public IActionResult Details(int id)
        {
            DetailsOfferViewModel model;
            try
            {
                model = this.offerService.GetOfferById(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OfferController.Index));
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            DisplayEditOfferViewModel model;
            try
            {
                model = this.offerService.GetOfferToEdit(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OfferController.Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DisplayEditOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            try
            {
                this.offerService.EditOffer(model);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OfferController.Index));
            }

            RedirectToActionResult redirectResult = MakeRedirectResult(nameof(Areas.User), nameof(OfferController), nameof(OfferController.Details), model.OfferModel.Id);

            return redirectResult;
        }

        [HttpPost]
        public IActionResult Like(int offerId)
        {

            string currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                bool result = this.offerService.AddLikeToOffer(offerId, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OfferController.Index));
            }

            return RedirectToAction(nameof(OfferController.Index));
        }

        [HttpPost]
        public IActionResult DisLike(int offerId)
        {

            string currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                bool result = this.offerService.AddDisLikeToOffer(offerId, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OfferController.Index));
            }

            return RedirectToAction(nameof(OfferController.Index));
        }
    }
}
