namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.Web.Areas.Identity.Data;

    public class AnnouncementController : Controller
    {

        private readonly IAnnouncementService announcementService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public AnnouncementController(IAnnouncementService announcementService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.announcementService = announcementService;
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
            this.announcementService.Create(model, currentUserId);

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        public IActionResult All()
        {
            var announcements = this.announcementService.GetAllAnnouncements();
            var result = new DisplayAllAnnouncementsViewModel
            {
                Announcements = announcements
            }
            ;
            return View(result);
        }

        public IActionResult MyAnnouncements()
        {

            string currentUserId = this.userManager.GetUserId(this.User);
            var announcements = this.announcementService.GetMyAnnouncements(currentUserId);
            var result = new DisplayAllAnnouncementsViewModel
            {
                Announcements = announcements
            }
            ;
            return View(result);
        }

        public IActionResult Details(int id)
        {
            ViewBag.id = id;
            return View();
        }


        public IActionResult Delete(int id)
        {
            this.announcementService.Delete(id);
            return this.RedirectToAction("Index", "Home");
        }
    }
}
