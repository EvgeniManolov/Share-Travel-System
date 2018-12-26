namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Web.Areas.Identity.Data;

    public class ReviewController : Controller
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
        public IActionResult Create(string comment, int offerId)
        {
            string currentUserId = this.userManager.GetUserId(this.User);
            this.reviewService.CreateReview(comment, offerId, currentUserId);
            return this.Redirect("/offer/details/" + offerId);
        }
    }
}
