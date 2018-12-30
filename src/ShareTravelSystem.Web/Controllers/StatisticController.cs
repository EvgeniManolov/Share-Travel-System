namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels.Statistic;
    using System.Collections;
    using System.Collections.Generic;

    public class StatisticController : BaseController
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
