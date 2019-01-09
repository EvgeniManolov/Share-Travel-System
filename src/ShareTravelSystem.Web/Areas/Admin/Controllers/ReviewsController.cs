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
        private readonly IReviewService _reviewService;
        private readonly UserManager<ShareTravelSystemUser> _userManager;

        public ReviewsController(IReviewService reviewService, UserManager<ShareTravelSystemUser> userManager)
        {
            this._reviewService = reviewService;
            this._userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int reviewId, int offerId)
        {
            try
            {
                await _reviewService.DeleteReviewAsync(reviewId, offerId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(OffersController.Index));
            }

            var redirectResult = MakeRedirectResult(nameof(Admin),
                nameof(OffersController),
                nameof(OffersController.Details),
                offerId);

            return redirectResult;
        }
    }
}