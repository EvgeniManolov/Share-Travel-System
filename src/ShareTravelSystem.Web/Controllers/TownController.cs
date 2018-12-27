namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Town;
    using ShareTravelSystem.Web.Areas.Identity.Data;

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
            if (page == 0) page = 1;
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
        public IActionResult Create(CrateTownViewModel model)
        {
            this.townService.Create(model);

            return RedirectToAction(nameof(TownController.All), "Town");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            EditTownViewModel town = this.townService.GetTownById(id);
            return View(town);
        }

        [HttpPost]
        public IActionResult Edit(EditTownViewModel model)
        {
            this.townService.EditTownById(model);

            return RedirectToAction(nameof(TownController.All), "Town");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            this.townService.Delete(id);
            return RedirectToAction(nameof(TownController.All), "Town");
        }
    }
}
