namespace ShareTravelSystem.Web.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Announcement;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Controllers;
    using System;
    using System.Threading.Tasks;

    [Area("User")]
    [Authorize(Roles = "User")]
    public class AnnouncementController : BaseController
    {
        private readonly IAnnouncementService announcementService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public AnnouncementController(IAnnouncementService announcementService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.announcementService = announcementService;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Index(string search, bool privateAnnouncements, int page)
        {
            string currentUserId = this.userManager.GetUserId(this.User);
            var result = await this.announcementService.GetAllAnnouncementsAsync(privateAnnouncements, search, currentUserId, page);

            ViewData["Title"] = result.TitleOfPage;

            return this.View(result);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAnnouncementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            string currentUserId = this.userManager.GetUserId(this.User);
            await this.announcementService.CreateAnnouncementAsync(model, currentUserId);

            return RedirectToAction(nameof(AnnouncementController.Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            DetailsAnnouncementViewModel model;

            try
            {
                model = await this.announcementService.DetailsAnnouncementAsync(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(AnnouncementController.Index));
            }

            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            string currentUserId = this.userManager.GetUserId(this.User);
            EditAnnouncementViewModel model;
            try
            {
                model = await this.announcementService.GetAnnouncementToEditAsync(id, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(AnnouncementController.Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAnnouncementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.announcementService.EditAnnouncementAsync(model);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.View(model);
            }
            RedirectToActionResult redirectResult = MakeRedirectResult(nameof(Areas.User), nameof(AnnouncementController), nameof(AnnouncementController.Details), model.Id);
            return redirectResult;
        }
    }
}
