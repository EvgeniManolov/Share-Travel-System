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
            TownPaginationViewModel towns = await this.townService.GetAllTownsAsync(page, search);
            return View(towns);
        }
        
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Create(CrateTownViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
               await this.townService.CreateTownAsync(model);
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
            EditTownViewModel town;

            try
            {
                town = await this.townService.GetTownToEditAsync(id);
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
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
               await this.townService.EditTownAsync(model);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Name", e.Message);
                return this.View(model);
            }

            return RedirectToAction(nameof(TownController.Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
               await  this.townService.DeleteTownAsync(id);
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
