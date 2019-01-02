namespace ShareTravelSystem.Web.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels.Review;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Controllers;
    using System;

    [Area("User")]
    [Authorize(Roles = "User")]
    public class ReviewController : BaseController
    {

        private readonly IReviewService reviewService;
        private readonly IOfferService offerService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public ReviewController(IReviewService reviewService,
                                IOfferService offerService,
                                UserManager<ShareTravelSystemUser> userManager)
        {
            this.reviewService = reviewService;
            this.offerService = offerService;
            this.userManager = userManager;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string comment, int offerId)
        {
            string currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                this.reviewService.CreateReview(comment, offerId, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OfferController.Index));
            }

            RedirectToActionResult redirectResult = MakeRedirectResult(nameof(Areas.User), nameof(OfferController),
                                                                        nameof(OfferController.Details),
                                                                        offerId);
            return redirectResult;
        }


        public IActionResult Edit(int id, int offerId)
        {
            EditReviewViewModel model;
            string currentUserId = this.userManager.GetUserId(this.User);
            try
            {
                model = this.reviewService.GetToEditReviewById(id, offerId, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OfferController.Index));
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Edit(EditReviewViewModel model)
        {

            try
            {
                this.reviewService.EditReviewById(model);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OfferController.Index));
            }

            RedirectToActionResult redirectResult = MakeRedirectResult(nameof(Areas.User),
                                                                        nameof(OfferController),
                                                                        nameof(OfferController.Details),
                                                                        model.OfferId);
           
            return redirectResult;
        }
    }
}
