namespace ShareTravelSystem.Services
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Services.Infrastructure;
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

        public AnnouncementService(ShareTravelSystemDbContext db,
                                    UserManager<ShareTravelSystemUser> userManager)
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

        public IEnumerable<DisplayAnnouncementViewModel> GetIndexAnnouncements()
        {
            return this.db.Announcements
                        .OrderByDescending(x => x.CreateDate)
                        .ProjectTo<DisplayAnnouncementViewModel>()
                        .Take(4)
                        .ToList();
        }

        public AnnouncementPaginationViewModel GetAllAnnouncements(bool privateAnnouncements, string search, string currentUserId, int page)
        {
            int size = Constants.AnnouncementsPerPage;
            if (page == 0) page = 1;
            List<DisplayAnnouncementViewModel> announcements = new List<DisplayAnnouncementViewModel>();
            string titleOfPage = "";

            if (!privateAnnouncements)
            {
                announcements = this.db.Announcements
                         .OrderByDescending(x => x.CreateDate)
                         .ProjectTo<DisplayAnnouncementViewModel>()
                         .ToList();

                titleOfPage = "All Announcements";
            }
            else
            {
                announcements = this.db.Announcements
                         .Where(o => o.AuthorId == currentUserId)
                         .OrderByDescending(x => x.CreateDate)
                         .ProjectTo<DisplayAnnouncementViewModel>()
                         .ToList();

                titleOfPage = "My Announcements";
            }

            if (search != null && search != "")
            {
                announcements = announcements
                                .Where(x => x.Content.ToLower().Contains(search.Trim().ToLower())
                                         || x.Title.ToLower().Contains(search.Trim().ToLower()))
                                 .ToList();
            }

            int count = announcements.Count();
            announcements = announcements.Skip((page - 1) * size).Take(size).ToList();

            AnnouncementPaginationViewModel result = new AnnouncementPaginationViewModel
            {
                Search = search,
                Size = size,
                Page = page,
                Count = count,
                TitleOfPage = titleOfPage,
                PrivateAnnouncements = privateAnnouncements,
                AllAnnouncements = new DisplayAllAnnouncementsViewModel
                {
                    Announcements = announcements
                }
            };

            return result;
        }


        public void Delete(int Id)
        {
            Announcement announcement = this.db
                                            .Announcements
                                            .FirstOrDefault(p => p.Id == Id);
            if (announcement == null)
            {
                throw new ArgumentException(string.Format(Constants.AnnouncementDoesNotExist, Id));
            }

            this.db.Announcements.Remove(announcement);
            this.db.SaveChanges();
        }

        public DetailsAnnouncementViewModel DetailsAnnouncementById(int id)
        {
            DetailsAnnouncementViewModel result = this.db
                       .Announcements
                       .Where(a => a.Id == id)
                       .ProjectTo<DetailsAnnouncementViewModel>()
                       .FirstOrDefault();
            if (result == null)
            {
                throw new ArgumentException(string.Format(Constants.AnnouncementDoesNotExist, id));
            }

            return result;
        }

        public EditAnnouncementViewModel EditAnnouncementById(int id)
        {
            EditAnnouncementViewModel result = this.db
                       .Announcements
                       .Where(a => a.Id == id)
                       .ProjectTo<EditAnnouncementViewModel>()
                       .FirstOrDefault();

            if (result == null)
            {
                throw new ArgumentException(string.Format(Constants.AnnouncementDoesNotExist, id));
            }

            return result;
        }

        public void EditAnnouncement(EditAnnouncementViewModel model)
        {
            Announcement announcement = this.db
                                            .Announcements
                                            .FirstOrDefault(p => p.Id == model.Id);
            if (announcement == null)
            {
                throw new ArgumentException(string.Format(Constants.AnnouncementDoesNotExist, model.Id));
            }

            announcement.Title = model.Title;
            announcement.Content = model.Content;

            this.db.SaveChanges();
        }
    }
}
