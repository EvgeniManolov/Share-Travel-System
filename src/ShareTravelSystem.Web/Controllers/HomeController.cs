namespace ShareTravelSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Contracts;
    using ViewModels;

    public class HomeController : BaseController
    {
        private readonly IAnnouncementService announcementService;
        private readonly IMessageService messageService;

        public HomeController(IAnnouncementService announcementService, IMessageService messageService)
        {
            this.announcementService = announcementService;
            this.messageService = messageService;
        }


        public async Task<IActionResult> Chat()
        {
            var messages = await this.messageService.GetAllMessagesAsync();
            return this.View(messages);
        }

        public async Task<IActionResult> Index()
        {

            var announcements = await this.announcementService.GetIndexAnnouncementsAsync();
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
