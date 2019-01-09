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
        private readonly IOfferService _offerService;
        private readonly UserManager<ShareTravelSystemUser> _userManager;

        public OffersController(IOfferService offerService, UserManager<ShareTravelSystemUser> userManager)
        {
            this._offerService = offerService;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index(string search, bool privateOffers, int page,
            string filter = Constants.FilterOfAllOffers)
        {
            var currentUserId = _userManager.GetUserId(User);
            var result =
                await _offerService.GetAllOffersAsync(privateOffers, filter, search, currentUserId, page);
            var likedOffersIds = _offerService.GetLikedOrDislikedOffersIds(currentUserId).ToList();

            ViewData["LikedDislikedOffersIds"] = likedOffersIds;
            ViewData["Title"] = result.TitleOfPage;
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            DetailsOfferViewModel model;
            try
            {
                model = await _offerService.DetailsOfferAsync(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _offerService.DeleteOfferAsync(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}