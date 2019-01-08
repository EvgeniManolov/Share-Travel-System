namespace ShareTravelSystem.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ViewModels.Messages;
    using Web.Areas.Identity.Data;
    using Web.Models;

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
