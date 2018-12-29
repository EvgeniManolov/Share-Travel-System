namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;

    public class StatisticController : BaseController
    {

        private readonly IOfferService offerService;

        public StatisticController(IOfferService offerService)
        {
            this.offerService = offerService;
        }


        [HttpGet]
        [Authorize]
        public IActionResult StatisticByRating()
        {
            return null;
        }
    }
}
