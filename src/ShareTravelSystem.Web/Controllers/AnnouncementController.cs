namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.Web.Areas.Identity.Data;

    public class AnnouncementController : Controller
    {

        private readonly IAnnouncementService announcements;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public AnnouncementController(IAnnouncementService announcements, UserManager<ShareTravelSystemUser> userManager)
        {
            this.announcements = announcements;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAnnouncementViewModel model, string returnUrl = null)
        {
            string currentUserId = this.userManager.GetUserId(this.User);
            this.announcements.Create(model, currentUserId);

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }
    }
}
