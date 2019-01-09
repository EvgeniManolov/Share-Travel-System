namespace ShareTravelSystem.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using ViewModels;
    using Web.Controllers;

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : BaseController
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            this._statisticService = statisticService;
        }

        public async Task<IActionResult> StatisticByRating(int page, string search)
        {
            var model =
                await _statisticService.GetStatisticForAllUsersByRatingAsync(page, search);
            return View(model);
        }
    }
}