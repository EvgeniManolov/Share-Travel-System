namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.Web.Controllers;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StatisticController: BaseController
    {
        private readonly IStatisticService statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }
        
        public IActionResult StatisticByRating(int page, string search)
        {
            StatisticByRatingPaginationViewModel model = this.statisticService.GetStatisticForAllUsersByRating(page,search);
            return this.View(model);
        }
    }
}
