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
    public class AnnouncementsController : BaseController
    {
        private readonly IAnnouncementService _announcementService;
        private readonly UserManager<ShareTravelSystemUser> _userManager;

        public AnnouncementsController(IAnnouncementService announcementService,
            UserManager<ShareTravelSystemUser> userManager)
        {
            this._announcementService = announcementService;
            this._userManager = userManager;
        }


        public async Task<IActionResult> Index(string search, bool privateAnnouncements, int page)
        {
            var currentUserId = _userManager.GetUserId(User);
            var result =
                await _announcementService.GetAllAnnouncementsAsync(privateAnnouncements, search, currentUserId,
                    page);

            ViewData["Title"] = result.TitleOfPage;
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            DetailsAnnouncementViewModel model;

            try
            {
                model = await _announcementService.DetailsAnnouncementAsync(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
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
                await _announcementService.DeleteAnnouncementAsync(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}