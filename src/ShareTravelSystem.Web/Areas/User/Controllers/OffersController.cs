namespace ShareTravelSystem.Web.Areas.User.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Identity.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using Services.Infrastructure;
    using ViewModels.Offer;
    using Web.Controllers;

    [Area("User")]
    [Authorize(Roles = "User")]
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


        public IActionResult Create()
        {
            var towns = _offerService.GetAllTowns().ToList();
            ViewData["Towns"] = towns;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = _userManager.GetUserId(User);
            try
            {
                await _offerService.CreateOfferAsync(model, currentUserId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
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

        public async Task<IActionResult> Edit(int id)
        {
            DisplayEditOfferViewModel model;
            var currentUserId = _userManager.GetUserId(User);
            try
            {
                model = await _offerService.GetOfferToEditAsync(id, currentUserId);
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
        public async Task<IActionResult> Edit(DisplayEditOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _offerService.EditOfferAsync(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }

            var redirectResult = MakeRedirectResult(nameof(Areas.User), nameof(OffersController),
                nameof(Details), model.OfferModel.Id);

            return redirectResult;
        }

        [HttpPost]
        public async Task<IActionResult> Like(int offerId)
        {
            var currentUserId = _userManager.GetUserId(User);
            try
            {
                var result = await _offerService.LikeOfferAsync(offerId, currentUserId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DisLike(int offerId)
        {
            var currentUserId = _userManager.GetUserId(User);
            try
            {
                var result = await _offerService.DisLikeOfferAsync(offerId, currentUserId);
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