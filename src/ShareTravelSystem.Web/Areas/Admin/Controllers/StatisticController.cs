namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels.Statistic;
    using ShareTravelSystem.Web.Controllers;

    [Area("Admin")]
    public class StatisticController: BaseController
    {
        private readonly IStatisticService statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult StatisticByRating()
        {
            StatisticByRating model = this.statisticService.GetStatisticForAllUsersByRating();
            return this.View(model);
        }
    }
}
