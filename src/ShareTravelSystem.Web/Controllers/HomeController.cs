namespace ShareTravelSystem.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.Web.Models;

    public class HomeController : Controller
    {
        private readonly IAnnouncementService announcementService;

        public HomeController(IAnnouncementService announcementService)
        {
            this.announcementService = announcementService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var announcements = this.announcementService.GetIndexAnnouncements();
            var result = new DisplayAllAnnouncementsViewModel
            {
                Announcements = announcements
            }
            ;
            return View(result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
