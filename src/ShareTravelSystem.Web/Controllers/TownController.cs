namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Town;
    using System;
    using ShareTravelSystem.Web.Infrastructure.Constants;

    public class TownController : BaseController
    {
        private const string redirectToPath = "/Account/Login";
        private readonly ITownService townService;

        public TownController(ITownService townService)
        {
            this.townService = townService;
        }

        [HttpGet]
        public IActionResult All(int page, string search)
        {
            if (!RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(redirectToPath);
            };

            int size = Constants.TownsPerPage;
            TownPaginationViewModel towns = this.townService.GetAllTowns(size, page, search);

            return View(towns);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(redirectToPath);
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CrateTownViewModel model)
        {
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(redirectToPath);
            };

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
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(redirectToPath);
            };

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
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(redirectToPath);
            };

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
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(redirectToPath);
            }

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
