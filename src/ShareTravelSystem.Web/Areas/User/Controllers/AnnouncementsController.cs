namespace ShareTravelSystem.Web.Areas.User.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Identity.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using ViewModels.Announcement;
    using Web.Controllers;

    [Area("User")]
    [Authorize(Roles = "User")]
    public class AnnouncementsController : BaseController
    {
        private readonly IAnnouncementService announcementService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public AnnouncementsController(IAnnouncementService announcementService,
            UserManager<ShareTravelSystemUser> userManager)
        {
            this.announcementService = announcementService;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Index(string search, bool privateAnnouncements, int page)
        {
            var currentUserId = this.userManager.GetUserId(this.User);
            var result =
                await this.announcementService.GetAllAnnouncementsAsync(privateAnnouncements, search, currentUserId,
                    page);

            this.ViewData["Title"] = result.TitleOfPage;

            return this.View(result);
        }


        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAnnouncementViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var currentUserId = this.userManager.GetUserId(this.User);
            await this.announcementService.CreateAnnouncementAsync(model, currentUserId);

            return this.RedirectToAction(nameof(this.Index));
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
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var currentUserId = this.userManager.GetUserId(this.User);
            EditAnnouncementViewModel model;
            try
            {
                model = await this.announcementService.GetAnnouncementToEditAsync(id, currentUserId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAnnouncementViewModel model)
        {
            if (!this.ModelState.IsValid)
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

            var redirectResult = this.MakeRedirectResult(nameof(Areas.User),
                nameof(AnnouncementsController), nameof(this.Details), model.Id);
            return redirectResult;
        }
    }
}