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


        public IActionResult Create()
        {
            var towns = this.offerService.GetAllTowns().ToList();
            this.ViewData["Towns"] = towns;
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOfferViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                await this.offerService.CreateOfferAsync(model, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.RedirectToAction(nameof(this.Index));
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

        public async Task<IActionResult> Edit(int id)
        {
            DisplayEditOfferViewModel model;
            var currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                model = await this.offerService.GetOfferToEditAsync(id, currentUserId);
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
        public async Task<IActionResult> Edit(DisplayEditOfferViewModel model)
        {
            if (!this.ModelState.IsValid)
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
                return this.RedirectToAction(nameof(this.Index));
            }

            var redirectResult = this.MakeRedirectResult(nameof(Areas.User), nameof(OffersController),
                nameof(this.Details), model.OfferModel.Id);

            return redirectResult;
        }

        [HttpPost]
        public async Task<IActionResult> Like(int offerId)
        {
            var currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                var result = await this.offerService.LikeOfferAsync(offerId, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<IActionResult> DisLike(int offerId)
        {
            var currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                var result = await this.offerService.DisLikeOfferAsync(offerId, currentUserId);
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