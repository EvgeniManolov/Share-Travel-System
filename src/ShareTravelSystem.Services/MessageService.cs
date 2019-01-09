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
    using ShareTravelSystem.ViewModels;
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

        public async Task<MessagePaginationViewModel> GetAllMessagesAsync(string search, int page)
        {
            int size = 10;
            if (page == 0) page = 1;

            var messages = await this.db.Messages.OrderByDescending(x => x.CreateOn).ProjectTo<DisplayMessageViewModel>().ToListAsync();
            if (search != null && search != "")
            {
                messages = messages.Where(x => x.Text.ToLower().Contains(search.Trim().ToLower()) || x.Author.ToLower().Contains(search.Trim().ToLower())).ToList();
            }
            int count = messages.Count();
            messages = messages.Skip((page - 1) * size).Take(size).ToList();
            DisplayAllMessagesViewModel model = new DisplayAllMessagesViewModel { Messages = messages };
            MessagePaginationViewModel result = new MessagePaginationViewModel { Search = search, Size = size, Page = page, Count = count, Messages = model };

            return result;
        }
    }
}
