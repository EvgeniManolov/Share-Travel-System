namespace ShareTravelSystem.Services
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Announcement;
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
            return this.db.Announcements.OrderByDescending(x => x.CreateDate).ProjectTo<DisplayAnnouncementViewModel>().Take(4).ToList();
        }

        public List<DisplayAnnouncementViewModel> GetAllAnnouncements()
        {
            return this.db.Announcements.OrderByDescending(x => x.CreateDate).ProjectTo<DisplayAnnouncementViewModel>().ToList();

        }

        public List<DisplayAnnouncementViewModel> GetMyAnnouncements(string userId)
        {
            return this.db.Announcements.Where(a => a.AuthorId == userId).OrderByDescending(x => x.CreateDate).ProjectTo<DisplayAnnouncementViewModel>().ToList();
        }

        public void Delete(int announcementId)
        {
            var announcement = this.db.Announcements.FirstOrDefault(p => p.Id == announcementId);

            this.db.Announcements.Remove(announcement);

            this.db.SaveChanges();
        }

        public DetailsAnnouncementViewModel DetailsAnnouncementById(int id)
        {
            return this.db.Announcements.Where(a => a.Id == id).ProjectTo<DetailsAnnouncementViewModel>().FirstOrDefault();
        }

        public EditAnnouncementViewModel EditAnnouncementById(int id)
        {
            return this.db.Announcements.Where(a => a.Id == id).ProjectTo<EditAnnouncementViewModel>().FirstOrDefault();
        }

        public void EditAnnouncement(EditAnnouncementViewModel model)
        {
            var announcement = this.db.Announcements.FirstOrDefault(p => p.Id == model.Id);

            announcement.Title = model.Title;
            announcement.Content = model.Content;

            this.db.SaveChanges();
        }
    }
}
