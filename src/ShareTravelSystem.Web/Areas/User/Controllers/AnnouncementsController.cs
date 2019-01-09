namespace ShareTravelSystem.Web.Areas.User.Controllers
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

    [Area("User")]
    [Authorize(Roles = "User")]
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
                return View(model);
            }

            var currentUserId = _userManager.GetUserId(User);
            await _announcementService.CreateAnnouncementAsync(model, currentUserId);

            return RedirectToAction(nameof(Index));
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


        public async Task<IActionResult> Edit(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            EditAnnouncementViewModel model;
            try
            {
                model = await _announcementService.GetAnnouncementToEditAsync(id, currentUserId);
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
        public async Task<IActionResult> Edit(EditAnnouncementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _announcementService.EditAnnouncementAsync(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return View(model);
            }

            var redirectResult = MakeRedirectResult(nameof(Areas.User),
                nameof(AnnouncementsController), nameof(Details), model.Id);
            return redirectResult;
        }
    }
}