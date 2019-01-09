namespace ShareTravelSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Contracts;
    using ViewModels.Announcement;

    public class HomeController : BaseController
    {
        private readonly IAnnouncementService announcementService;
        private readonly IMessageService messageService;

        public HomeController(IAnnouncementService announcementService, IMessageService messageService)
        {
            this.announcementService = announcementService;
            this.messageService = messageService;
        }


        public async Task<IActionResult> Chat(string search, int page)
        {
            var messages = await this.messageService.GetAllMessagesAsync(search, page);
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
            return this.View(result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier});
        }
    }
}