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
    public class StatisticController : BaseController
    {
        private readonly IStatisticService statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        public async Task<IActionResult> StatisticByRating(int page, string search)
        {
            StatisticByRatingPaginationViewModel model = await this.statisticService.GetStatisticForAllUsersByRatingAsync(page, search);
            return this.View(model);
        }
    }
}
