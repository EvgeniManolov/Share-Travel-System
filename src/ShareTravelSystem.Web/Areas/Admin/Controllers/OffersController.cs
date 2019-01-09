namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Identity.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using Services.Infrastructure;
    using ViewModels;
    using ViewModels.Offer;
    using Web.Controllers;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OffersController : BaseController
    {
        private readonly IOfferService offerService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public OffersController(IOfferService offerService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.offerService = offerService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string search, bool privateOffers, int page,
            string filter = Constants.FilterOfAllOffers)
        {
            var currentUserId = this.userManager.GetUserId(this.User);
            var result =
                await this.offerService.GetAllOffersAsync(privateOffers, filter, search, currentUserId, page);
            var likedOffersIds = this.offerService.GetLikedOrDislikedOffersIds(currentUserId).ToList();

            this.ViewData["LikedDislikedOffersIds"] = likedOffersIds;
            this.ViewData["Title"] = result.TitleOfPage;
            return this.View(result);
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
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.offerService.DeleteOfferAsync(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}