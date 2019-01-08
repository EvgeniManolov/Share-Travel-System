namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Identity.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using ViewModels;
    using ViewModels.Announcement;
    using Web.Controllers;

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
        
        
        public async Task<IActionResult> Index(string search, bool privateAnnouncements, int page)
        {
            string currentUserId = this.userManager.GetUserId(this.User);
            AnnouncementPaginationViewModel result = await this.announcementService.GetAllAnnouncementsAsync(privateAnnouncements, search, currentUserId, page);

            ViewData["Title"] = result.TitleOfPage;
            return this.View(result);
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
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.announcementService.DeleteAnnouncementAsync(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
