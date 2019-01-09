namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using ViewModels.Town;
    using Web.Controllers;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TownsController : BaseController
    {
        private readonly ITownService _townService;

        public TownsController(ITownService townService)
        {
            this._townService = townService;
        }

        public async Task<IActionResult> Index(int page, string search)
        {
            var towns = await _townService.GetAllTownsAsync(page, search);
            return View(towns);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrateTownViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _townService.CreateTownAsync(model);
            }

            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            EditTownViewModel town;

            try
            {
                town = await _townService.GetTownToEditAsync(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);

                return RedirectToAction(nameof(Index));
            }

            return View(town);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTownViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _townService.EditTownAsync(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _townService.DeleteTownAsync(id);
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