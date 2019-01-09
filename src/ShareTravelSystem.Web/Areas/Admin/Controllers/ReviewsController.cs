namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Identity.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using Web.Controllers;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReviewsController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public ReviewsController(IReviewService reviewService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int reviewId, int offerId)
        {
            try
            {
                await this.reviewService.DeleteReviewAsync(reviewId, offerId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.RedirectToAction(nameof(OffersController.Index));
            }

            var redirectResult = this.MakeRedirectResult(nameof(Admin),
                nameof(OffersController),
                nameof(OffersController.Details),
                offerId);

            return redirectResult;
        }
    }
}