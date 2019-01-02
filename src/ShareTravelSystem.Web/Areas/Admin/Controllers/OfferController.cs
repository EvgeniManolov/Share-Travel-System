namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
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

    [Area("Admin")]
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

        [HttpPost]
        public IActionResult Delete(int id)
        {

            try
            {
                this.offerService.DeleteOfferById(id);
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
