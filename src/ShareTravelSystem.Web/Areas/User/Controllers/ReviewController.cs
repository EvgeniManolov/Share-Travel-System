namespace ShareTravelSystem.Web.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Controllers;
    using System;

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
        [Authorize]
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
                return RedirectToAction(nameof(OfferController.All));
            }

            RedirectToActionResult redirectResult = MakeRedirectResult(nameof(OfferController),
                                                                        nameof(OfferController.Details),
                                                                        offerId);
            return redirectResult;
        }
    }
}
