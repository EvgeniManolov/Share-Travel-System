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
        private readonly IAnnouncementService _announcementService;
        private readonly IMessageService _messageService;

        public HomeController(IAnnouncementService announcementService, IMessageService messageService)
        {
            this._announcementService = announcementService;
            this._messageService = messageService;
        }


        public async Task<IActionResult> Chat(string search, int page)
        {
            var messages = await _messageService.GetAllMessagesAsync(search, page);
            return View(messages);
        }

        public async Task<IActionResult> Index()
        {
            var announcements = await _announcementService.GetIndexAnnouncementsAsync();
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
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}