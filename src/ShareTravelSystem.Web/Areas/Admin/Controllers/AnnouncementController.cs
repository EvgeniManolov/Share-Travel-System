namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels.Announcement;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Controllers;
    using System;


    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
        
        public IActionResult Details(int id)
        {
            DetailsAnnouncementViewModel model;

            try
            {
                model = this.announcementService.DetailsAnnouncement(id);
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
        public IActionResult Delete(int id)
        {
            try
            {
                this.announcementService.DeleteAnnouncement(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(AnnouncementController.Index));
            }
            return RedirectToAction(nameof(AnnouncementController.Index));
        }
    }
}
