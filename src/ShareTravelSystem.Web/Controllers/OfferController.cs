namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Web.Areas.Identity.Data;

    public class OfferController : Controller
    {

        private readonly IOfferService offerService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public OfferController(IOfferService offerService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.offerService = offerService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
    }
}
