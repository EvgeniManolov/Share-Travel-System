namespace ShareTravelSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;
    using ViewModels.Announcement;
    using Web.Areas.Identity.Data;
    using Web.Models;

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


        public async Task CreateAnnouncementAsync(CreateAnnouncementViewModel model, string userid)
        {
            this.db.Announcements.Add(new Announcement
            {
                Title = model.Title,
                Content = model.Content,
                CreateDate = DateTime.UtcNow,
                AuthorId = userid
            });

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<DisplayAnnouncementViewModel>> GetIndexAnnouncementsAsync()
        {
            var result =  await this.db.Announcements
                .Where(a => !a.IsDeleted)
                        .OrderByDescending(x => x.CreateDate)
                        .ProjectTo<DisplayAnnouncementViewModel>()
                        .Take(4)
                        .ToListAsync();
            return result;
        }

        public async Task<AnnouncementPaginationViewModel> GetAllAnnouncementsAsync(bool privateAnnouncements, string search, string currentUserId, int page)
        {
            int size = Constants.AnnouncementsPerPage;
            if (page == 0) page = 1;
            List<DisplayAnnouncementViewModel> announcements = new List<DisplayAnnouncementViewModel>();
            string titleOfPage = "";

            if (!privateAnnouncements)
            {
                announcements = await this.db.Announcements
                         .Where(a => !a.IsDeleted)
                         .OrderByDescending(x => x.CreateDate)
                         .ProjectTo<DisplayAnnouncementViewModel>()
                         .ToListAsync();

                titleOfPage = "All Announcements";
            }
            else
            {
                announcements = await this.db.Announcements
                         .Where(o => o.AuthorId == currentUserId && !o.IsDeleted)
                         .OrderByDescending(x => x.CreateDate)
                         .ProjectTo<DisplayAnnouncementViewModel>()
                         .ToListAsync();

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


        public async Task DeleteAnnouncementAsync(int Id)
        {
            Announcement announcement = await this.db
                                            .Announcements
                                            .SingleOrDefaultAsync(p => p.Id == Id);
            if (announcement == null)
            {
                throw new ArgumentException(string.Format(Constants.AnnouncementDoesNotExist, Id));
            }
            announcement.IsDeleted = true;
            await this.db.SaveChangesAsync();
        }

        public async Task<DetailsAnnouncementViewModel> DetailsAnnouncementAsync(int id)
        {
            DetailsAnnouncementViewModel result = await this.db
                       .Announcements
                       .Where(a => a.Id == id && !a.IsDeleted)
                       .ProjectTo<DetailsAnnouncementViewModel>()
                       .SingleOrDefaultAsync();
            if (result == null)
            {
                throw new ArgumentException(string.Format(Constants.AnnouncementDoesNotExist, id));
            }

            return result;
        }

        public async Task<EditAnnouncementViewModel> GetAnnouncementToEditAsync(int id, string currentUserId)
        {
            EditAnnouncementViewModel result = await this.db
                       .Announcements
                       .Where(a => a.Id == id && !a.IsDeleted)
                       .ProjectTo<EditAnnouncementViewModel>()
                       .SingleOrDefaultAsync();

            if (result == null)
            {
                throw new ArgumentException(string.Format(Constants.AnnouncementDoesNotExist, id));
            }

            string announcementAuthor = await this.db.Announcements.Where(a => a.Id == id && !a.IsDeleted).Select(r => r.AuthorId).SingleOrDefaultAsync();
            if (announcementAuthor != currentUserId)
            {
                throw new ArgumentException(string.Format(Constants.NotAuthorizedForThisOperation, currentUserId));
            }

            return result;
        }

        public async Task EditAnnouncementAsync(EditAnnouncementViewModel model)
        {
            Announcement announcement = await this.db
                                            .Announcements
                                            .Where(p => p.Id == model.Id && !p.IsDeleted)
                                            .SingleOrDefaultAsync();
            if (announcement == null)
            {
                throw new ArgumentException(string.Format(Constants.AnnouncementDoesNotExist, model.Id));
            }

            announcement.Title = model.Title;
            announcement.Content = model.Content;

            await this.db.SaveChangesAsync();
        }
    }
}
