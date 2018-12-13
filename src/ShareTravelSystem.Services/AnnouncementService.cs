namespace ShareTravelSystem.Services
{
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AnnouncementService : IAnnouncementService
    {
        private readonly ShareTravelSystemDbContext db;

        private readonly UserManager<ShareTravelSystemUser> userManager;

        public AnnouncementService(ShareTravelSystemDbContext db, UserManager<ShareTravelSystemUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public void Create(CreateAnnouncementViewModel model, string userid)
        {

            this.db.Announcements.Add(new Announcement
            {
                Title = model.Title,
                Content = model.Content,
                CreateDate = DateTime.UtcNow,
                AuthorId = userid
            });


            this.db.SaveChanges();
        }

        public List<DisplayAnnouncementViewModel> GetIndexAnnouncements()
        {
            return this.db.Announcements.OrderByDescending(x => x.CreateDate).Select(x => new DisplayAnnouncementViewModel
            {
                Id = x.Id,
                Content = x.Content,
                Title = x.Title,
                CreateDate = x.CreateDate,
                Author = x.Author.ToString()
            }).Take(4).ToList();
        }

        public List<DisplayAnnouncementViewModel> GetAllAnnouncements()
        {
            return this.db.Announcements.OrderByDescending(x => x.CreateDate).Select(x => new DisplayAnnouncementViewModel
            {
                Id = x.Id,
                Content = x.Content,
                Title = x.Title,
                CreateDate = x.CreateDate,
                Author = x.Author.ToString()
            }).ToList();
        }

        public List<DisplayAnnouncementViewModel> GetMyAnnouncements(string userId)
        {
            return this.db.Announcements.Where(a => a.AuthorId == userId).OrderByDescending(x => x.CreateDate).Select(x => new DisplayAnnouncementViewModel
            {
                Id = x.Id,
                Content = x.Content,
                Title = x.Title,
                CreateDate = x.CreateDate,
                Author = x.Author.ToString()
            }).ToList();
        }

        public void Delete(int announcementId)
        {
            var announcement = this.db.Announcements.FirstOrDefault(p => p.Id == announcementId);

            this.db.Announcements.Remove(announcement);

            this.db.SaveChanges();
        }
    }
}
