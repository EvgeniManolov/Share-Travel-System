namespace ShareTravelSystem.Web.SignalRChat
{
    using System.Threading.Tasks;
    using Areas.Identity.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;
    using Services.Contracts;

    public class ChatHub : Hub
    {
        private readonly IMessageService messageService;

        private readonly UserManager<ShareTravelSystemUser> userManager;

        public ChatHub(IMessageService messageService, UserManager<ShareTravelSystemUser> userManager)
        {
            this.messageService = messageService;
            this.userManager = userManager;
        }
        public async Task Send(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            await this.messageService
                .CreateMessageAsync(message, this.userManager.GetUserId(this.Context.User));

            await this.Clients.All.SendAsync("NewMessage", new Message
            {
                User = this.Context.User.Identity.Name,
                Text = message
            });
        }
    }

    public class Message
    {
        public string User { get; set; }

        public string Text { get; set; }
    }
}
