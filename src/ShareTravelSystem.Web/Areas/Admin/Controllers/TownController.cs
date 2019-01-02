namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Town;
    using ShareTravelSystem.Web.Controllers;
    using ShareTravelSystem.Web.Infrastructure.Constants;

    [Area("Admin")]
    public class TownController : BaseController
    {
        private readonly ITownService townService;

        public TownController(ITownService townService)
        {
            this.townService = townService;
        }
        
        public IActionResult Index(int page, string search)
        {
            if (!RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(Constants.redirectToLoginPage);
            };

            TownPaginationViewModel towns = this.townService.GetAllTowns(page, search);
            return View(towns);
        }
        
        public IActionResult Create()
        {
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(Constants.redirectToLoginPage);
            };

            return View();
        }

        [HttpPost]
        public IActionResult Create(CrateTownViewModel model)
        {
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(Constants.redirectToLoginPage);
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

            return RedirectToAction(nameof(TownController.Index));
        }
        
        public IActionResult Edit(int id)
        {
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(Constants.redirectToLoginPage);
            };

            EditTownViewModel town;

            try
            {
                town = this.townService.GetTownById(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);

                return RedirectToAction(nameof(TownController.Index));

            }

            return View(town);
        }

        [HttpPost]
        public IActionResult Edit(EditTownViewModel model)
        {
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(Constants.redirectToLoginPage);
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

            return RedirectToAction(nameof(TownController.Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(Constants.redirectToLoginPage);
            }

            try
            {
                this.townService.Delete(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return RedirectToAction(nameof(TownController.Index));
            }

            return RedirectToAction(nameof(TownController.Index));
        }
    }
}
