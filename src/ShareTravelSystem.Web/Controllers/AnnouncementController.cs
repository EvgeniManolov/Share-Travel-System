namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using X.PagedList;

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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAnnouncementViewModel model)
        {
            string currentUserId = this.userManager.GetUserId(this.User);
            this.announcementService.Create(model, currentUserId);

            return RedirectToAction(nameof(AnnouncementController.All), "Announcement");

        }

        [HttpGet]
        public IActionResult All(string search, bool privateAnnouncements, int page)
        {
            if (page == 0) page = 1;
            int size = 8;
            string currentUserId = this.userManager.GetUserId(this.User);
            var result = this.announcementService.GetAllAnnouncements(privateAnnouncements, search, currentUserId, page, size);

            ViewData["Title"] = result.TitleOfPage;

            return this.View(result);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = this.announcementService.DetailsAnnouncementById(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)

        {
            var model = this.announcementService.EditAnnouncementById(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditAnnouncementViewModel model)
        {
            this.announcementService.EditAnnouncement(model);
            return this.RedirectToAction("All", "Announcement");
        }


        public IActionResult Delete(int id)
        {
            this.announcementService.Delete(id);
            return this.RedirectToAction("All", "Announcement");
        }
    }
}
