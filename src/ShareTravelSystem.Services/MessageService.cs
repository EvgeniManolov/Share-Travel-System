namespace ShareTravelSystem.Services
{
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Threading.Tasks;

    public class MessageService : IMessageService
    {
        private readonly ShareTravelSystemDbContext db;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public MessageService(ShareTravelSystemDbContext db,
                                    UserManager<ShareTravelSystemUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task CreateMessageAsync(string message, string userId)
        {
            Message currentMessage = new Message
            {
                Text = message,
                AuthorId = userId,
                CreateOn = DateTime.UtcNow
            };
            await this.db.Messages.AddAsync(currentMessage);
            await this.db.SaveChangesAsync();
        }
    }
}
