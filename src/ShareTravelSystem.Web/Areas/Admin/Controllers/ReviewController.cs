namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Controllers;
    using System;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public ReviewController(IReviewService reviewService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult Delete(int reviewId, int offerId)
        {
            try
            {
                this.reviewService.DeleteReviewById(reviewId, offerId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OfferController.Index));
            }

            RedirectToActionResult redirectResult = MakeRedirectResult(nameof(Areas.Admin),
                                                                        nameof(OfferController),
                                                                        nameof(OfferController.Details),
                                                                        offerId);

            return redirectResult;
        }
    }
}
