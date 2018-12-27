namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Town;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using System;

    [Authorize(Roles = "Admin")]
    public class TownController : Controller
    {
        private readonly ITownService townService;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public TownController(ITownService townService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.townService = townService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult All(int page, string search)
        {
            int size = 10;
            TownPaginationViewModel towns = this.townService.GetAllTowns(size, page, search);

            return View(towns);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CrateTownViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                this.townService.Create(model);
            }

            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);

                return this.View(model);
            }

            return RedirectToAction(nameof(TownController.All));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            EditTownViewModel town;
            try
            {
                town = this.townService.GetTownById(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);

                return RedirectToAction(nameof(TownController.All));

            }
            return View(town);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditTownViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                this.townService.EditTownById(model);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);

                return this.View(model);
            }

            return RedirectToAction(nameof(TownController.All));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                this.townService.Delete(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);

                return RedirectToAction(nameof(TownController.All));
            }
            return RedirectToAction(nameof(TownController.All));
        }
    }
}
