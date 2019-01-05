namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Services.Infrastructure;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Offer;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using X.PagedList;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
                return RedirectToAction(nameof(OfferController.Index));
            }
            return View(model);
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
                return RedirectToAction(nameof(OfferController.Index));
            }
            return RedirectToAction(nameof(OfferController.Index));
        }
    }
}
