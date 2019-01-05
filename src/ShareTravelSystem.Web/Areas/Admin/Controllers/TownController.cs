namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Town;
    using ShareTravelSystem.Web.Controllers;
    using ShareTravelSystem.Web.Infrastructure.Constants;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TownController : BaseController
    {
        private readonly ITownService townService;

        public TownController(ITownService townService)
        {
            this.townService = townService;
        }
        
        public async Task<IActionResult> Index(int page, string search)
        {
            if (!RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(Constants.redirectToLoginPage);
            };

            TownPaginationViewModel towns = await this.townService.GetAllTowns(page, search);
            return View(towns);
        }
        
        public  IActionResult Create()
        {
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(Constants.redirectToLoginPage);
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Create(CrateTownViewModel model)
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
               await this.townService.CreateTown(model);
            }

            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);

                return this.View(model);
            }

            return RedirectToAction(nameof(TownController.Index));
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(Constants.redirectToLoginPage);
            };

            EditTownViewModel town;

            try
            {
                town = await this.townService.GetTownToEdit(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);

                return RedirectToAction(nameof(TownController.Index));

            }

            return View(town);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTownViewModel model)
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
               await this.townService.EditTown(model);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.View(model);
            }

            return RedirectToAction(nameof(TownController.Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.RedirectIfNotInRole(Constants.AdminRole))
            {
                return Redirect(Constants.redirectToLoginPage);
            }

            try
            {
               await  this.townService.DeleteTown(id);
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
