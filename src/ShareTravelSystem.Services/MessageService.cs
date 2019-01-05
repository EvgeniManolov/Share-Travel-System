namespace ShareTravelSystem.Services
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels.Messages;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Threading.Tasks;
    using System.Linq;

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

        public async Task<DisplayAllMessagesViewModel> GetAllMessagesAsync()
        {
            var messages = await this.db.Messages.OrderByDescending(x => x.CreateOn).ProjectTo<DisplayMessageViewModel>().ToListAsync();
            DisplayAllMessagesViewModel result = new DisplayAllMessagesViewModel { Messages = messages };
            return result;
        }
    }
}
