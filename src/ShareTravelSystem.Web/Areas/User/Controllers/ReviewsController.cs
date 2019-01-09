﻿namespace ShareTravelSystem.Web.Areas.User.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Identity.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using ViewModels.Review;
    using Web.Controllers;

    [Area("User")]
    [Authorize(Roles = "User")]
    public class ReviewsController : BaseController
    {
        private readonly IReviewService _reviewService;
        private readonly IOfferService _offerService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public ReviewsController(IReviewService reviewService,
            IOfferService offerService,
            UserManager<ShareTravelSystemUser> userManager)
        {
            this._reviewService = reviewService;
            this._offerService = offerService;
            this.userManager = userManager;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string comment, int offerId)
        {
            var currentUserId = userManager.GetUserId(User);
            try
            {
                await _reviewService.CreateReviewAsync(comment, offerId, currentUserId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OffersController.Index));
            }

            var redirectResult = MakeRedirectResult(nameof(Areas.User), nameof(OffersController),
                nameof(OffersController.Details),
                offerId);
            return redirectResult;
        }


        public async Task<IActionResult> Edit(int id, int offerId)
        {
            EditReviewViewModel model;
            var currentUserId = userManager.GetUserId(User);
            try
            {
                model = await _reviewService.GetReviewToEditAsync(id, offerId, currentUserId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OffersController.Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditReviewViewModel model)
        {
            try
            {
                await _reviewService.EditReviewAsync(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OffersController.Index));
            }

            var redirectResult = MakeRedirectResult(nameof(Areas.User),
                nameof(OffersController),
                nameof(OffersController.Details),
                model.OfferId);

            return redirectResult;
        }
    }
}