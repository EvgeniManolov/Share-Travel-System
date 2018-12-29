namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Web.Infrastructure.Constants;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Offer;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class OfferController : BaseController
    {

        private readonly IOfferService offerService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public OfferController(IOfferService offerService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.offerService = offerService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Town> towns = this.offerService.GetAllTowns().ToList();
            ViewData["Towns"] = towns;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            string currentUserId = this.userManager.GetUserId(this.User);
            this.offerService.Create(model, currentUserId);
            return RedirectToAction(nameof(OfferController.All));
        }

        [HttpGet]
        public IActionResult All(string search, bool privateOffers, int page, string filter = Constants.FilterOfAllOffers)
        {
            int size = Constants.OffersPerPage;
            string currentUserId = this.userManager.GetUserId(this.User);
            OfferPaginationViewModel result = this.offerService.GetAllOffers(privateOffers, filter, search, currentUserId, page, size);
            List<int> likedOffersIds = this.offerService.GetLikedOrDislikedOffersIds(currentUserId);

            ViewData["LikedDislikedOffersIds"] = likedOffersIds;
            ViewData["Title"] = result.TitleOfPage;
            return this.View(result);
        }

        [HttpGet]
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
                return RedirectToAction(nameof(OfferController.All));
            }
            return View(model);
        }

        [HttpGet]
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
                return RedirectToAction(nameof(OfferController.All));
            }
            return View(model);
        }

        [HttpPost]
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
                return RedirectToAction(nameof(OfferController.All));
            }

            RedirectToActionResult redirectResult = MakeRedirectResult(nameof(OfferController), nameof(OfferController.Details), model.OfferModel.Id);

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
                return RedirectToAction(nameof(OfferController.All));
            }

            return RedirectToAction(nameof(OfferController.All));
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
                return RedirectToAction(nameof(OfferController.All));
            }

            return RedirectToAction(nameof(OfferController.All));
        }
    }
}
