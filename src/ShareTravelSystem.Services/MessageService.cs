namespace ShareTravelSystem.Services
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels.Messages;
    using Web.Areas.Identity.Data;
    using Web.Models;

    public class MessageService : IMessageService
    {
        private readonly ShareTravelSystemDbContext _db;
        private readonly UserManager<ShareTravelSystemUser> _userManager;

        public MessageService(ShareTravelSystemDbContext db,
            UserManager<ShareTravelSystemUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        public async Task CreateMessageAsync(string message, string userId)
        {
            var currentMessage = new Message
            {
                Text = message,
                AuthorId = userId,
                CreateOn = DateTime.UtcNow
            };
            await _db.Messages.AddAsync(currentMessage);
            await _db.SaveChangesAsync();
        }

        public async Task<MessagePaginationViewModel> GetAllMessagesAsync(string search, int page)
        {
            var size = 10;
            if (page == 0) page = 1;

            var messages = await _db.Messages.OrderByDescending(x => x.CreateOn)
                .ProjectTo<DisplayMessageViewModel>().ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                messages = messages.Where(x =>
                    x.Text.ToLower().Contains(search.Trim().ToLower()) ||
                    x.Author.ToLower().Contains(search.Trim().ToLower())).ToList();
            }

            var count = messages.Count();
            messages = messages.Skip((page - 1) * size).Take(size).ToList();
            var model = new DisplayAllMessagesViewModel {Messages = messages};
            var result = new MessagePaginationViewModel
            {
                Search = search,
                Size = size,
                Page = page,
                Count = count,
                Messages = model
            };

            return result;
        }
    }
}