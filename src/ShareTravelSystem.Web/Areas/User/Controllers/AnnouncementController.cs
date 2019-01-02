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


    [Area("User")]
    public class AnnouncementController : BaseController
    {
        private readonly IAnnouncementService announcementService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public AnnouncementController(IAnnouncementService announcementService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.announcementService = announcementService;
            this.userManager = userManager;
        }
        

        public IActionResult Index(string search, bool privateAnnouncements, int page)
        {
            string currentUserId = this.userManager.GetUserId(this.User);
            var result = this.announcementService.GetAllAnnouncements(privateAnnouncements, search, currentUserId, page);

            ViewData["Title"] = result.TitleOfPage;

            return this.View(result);
        }

        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(CreateAnnouncementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            string currentUserId = this.userManager.GetUserId(this.User);
            this.announcementService.Create(model, currentUserId);

            return RedirectToAction(nameof(AnnouncementController.Index));

        }
        

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
                return RedirectToAction(nameof(AnnouncementController.Index));
            }

            return View(model);
        }
        

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
                return RedirectToAction(nameof(AnnouncementController.Index));
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
            RedirectToActionResult redirectResult = MakeRedirectResult(nameof(AnnouncementController), nameof(AnnouncementController.Details), model.Id);
            return redirectResult;
        }
    }
}
