namespace ShareTravelSystem.Web.Areas.User.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Identity.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using Services.Infrastructure;
    using ViewModels;
    using ViewModels.Offer;
    using Web.Controllers;

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

        public async Task<IActionResult> Index(string search, bool privateOffers, int page, string filter = Constants.FilterOfAllOffers)
        {
            
            string currentUserId = this.userManager.GetUserId(this.User);
            OfferPaginationViewModel result = await this.offerService.GetAllOffersAsync(privateOffers, filter, search, currentUserId, page);
            List<int> likedOffersIds = this.offerService.GetLikedOrDislikedOffersIds(currentUserId).ToList();

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
        public async Task<IActionResult> Create(CreateOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            string currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                await this.offerService.CreateOfferAsync(model, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            DetailsOfferViewModel model;
            try
            {
                model = await this.offerService.DetailsOfferAsync(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            DisplayEditOfferViewModel model;
            string currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                model = await this.offerService.GetOfferToEditAsync(id, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DisplayEditOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            try
            {
                await this.offerService.EditOfferAsync(model);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }

            RedirectToActionResult redirectResult = MakeRedirectResult(nameof(Areas.User), nameof(OfferController), nameof(Details), model.OfferModel.Id);

            return redirectResult;
        }

        [HttpPost]
        public async Task<IActionResult> Like(int offerId)
        {

            string currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                bool result = await this.offerService.LikeOfferAsync(offerId, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DisLike(int offerId)
        {

            string currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                bool result = await this.offerService.DisLikeOfferAsync(offerId, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
