namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Announcement;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using System;
    using ShareTravelSystem.Web.Infrastructure.Constants;

    public class AnnouncementController : BaseController
    {

        private readonly IAnnouncementService announcementService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public AnnouncementController(IAnnouncementService announcementService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.announcementService = announcementService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateAnnouncementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            string currentUserId = this.userManager.GetUserId(this.User);
            this.announcementService.Create(model, currentUserId);

            return RedirectToAction(nameof(AnnouncementController.All));

        }

        [HttpGet]
        [Authorize]
        public IActionResult All(string search, bool privateAnnouncements, int page)
        {
            int size = Constants.AnnouncementsPerPage;
            string currentUserId = this.userManager.GetUserId(this.User);
            var result = this.announcementService.GetAllAnnouncements(privateAnnouncements, search, currentUserId, page, size);

            ViewData["Title"] = result.TitleOfPage;

            return this.View(result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            DetailsAnnouncementViewModel model;

            try
            {
                model = this.announcementService.DetailsAnnouncementById(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(AnnouncementController.All));
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            EditAnnouncementViewModel model;
            try
            {
                model = this.announcementService.EditAnnouncementById(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(AnnouncementController.All));
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EditAnnouncementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                this.announcementService.EditAnnouncement(model);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.View(model);
            }

            return RedirectToAction(nameof(AnnouncementController.All));
        }
        

        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                this.announcementService.Delete(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(AnnouncementController.All));
            }
            return RedirectToAction(nameof(AnnouncementController.All));
        }
    }
}
