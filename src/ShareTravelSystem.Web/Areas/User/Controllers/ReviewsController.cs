namespace ShareTravelSystem.Web.Areas.User.Controllers
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
        private readonly IReviewService reviewService;
        private readonly IOfferService offerService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public ReviewsController(IReviewService reviewService,
            IOfferService offerService,
            UserManager<ShareTravelSystemUser> userManager)
        {
            this.reviewService = reviewService;
            this.offerService = offerService;
            this.userManager = userManager;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string comment, int offerId)
        {
            var currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                await this.reviewService.CreateReviewAsync(comment, offerId, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.RedirectToAction(nameof(OffersController.Index));
            }

            var redirectResult = this.MakeRedirectResult(nameof(Areas.User), nameof(OffersController),
                nameof(OffersController.Details),
                offerId);
            return redirectResult;
        }


        public async Task<IActionResult> Edit(int id, int offerId)
        {
            EditReviewViewModel model;
            var currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                model = await this.reviewService.GetReviewToEditAsync(id, offerId, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.RedirectToAction(nameof(OffersController.Index));
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditReviewViewModel model)
        {
            try
            {
                await this.reviewService.EditReviewAsync(model);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.RedirectToAction(nameof(OffersController.Index));
            }

            var redirectResult = this.MakeRedirectResult(nameof(Areas.User),
                nameof(OffersController),
                nameof(OffersController.Details),
                model.OfferId);

            return redirectResult;
        }
    }
}