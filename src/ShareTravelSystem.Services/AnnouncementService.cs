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
        private readonly ShareTravelSystemDbContext _db;
        private readonly UserManager<ShareTravelSystemUser> _userManager;

        public AnnouncementService(ShareTravelSystemDbContext db,
            UserManager<ShareTravelSystemUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }


        public async Task CreateAnnouncementAsync(CreateAnnouncementViewModel model, string userid)
        {
            _db.Announcements.Add(new Announcement
            {
                Title = model.Title,
                Content = model.Content,
                CreateDate = DateTime.UtcNow,
                AuthorId = userid
            });

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<DisplayAnnouncementViewModel>> GetIndexAnnouncementsAsync()
        {
            var result = await _db.Announcements
                .Where(a => !a.IsDeleted)
                .OrderByDescending(x => x.CreateDate)
                .ProjectTo<DisplayAnnouncementViewModel>()
                .Take(4)
                .ToListAsync();
            return result;
        }

        public async Task<AnnouncementPaginationViewModel> GetAllAnnouncementsAsync(bool privateAnnouncements,
            string search, string currentUserId, int page)
        {
            var size = Constants.AnnouncementsPerPage;
            if (page == 0) page = 1;
            List<DisplayAnnouncementViewModel> announcements;
            var titleOfPage = "";

            if (!privateAnnouncements)
            {
                announcements = await _db.Announcements
                    .Where(a => !a.IsDeleted)
                    .OrderByDescending(x => x.CreateDate)
                    .ProjectTo<DisplayAnnouncementViewModel>()
                    .ToListAsync();

                titleOfPage = "All Announcements";
            }
            else
            {
                announcements = await _db.Announcements
                    .Where(o => o.AuthorId == currentUserId && !o.IsDeleted)
                    .OrderByDescending(x => x.CreateDate)
                    .ProjectTo<DisplayAnnouncementViewModel>()
                    .ToListAsync();

                titleOfPage = "My Announcements";
            }

            if (!string.IsNullOrEmpty(search))
            {
                announcements = announcements
                    .Where(x => x.Content.ToLower().Contains(search.Trim().ToLower())
                                || x.Title.ToLower().Contains(search.Trim().ToLower()))
                    .ToList();
            }

            var count = announcements.Count();
            announcements = announcements.Skip((page - 1) * size).Take(size).ToList();

            var result = new AnnouncementPaginationViewModel
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
            var announcement = await _db
                .Announcements
                .SingleOrDefaultAsync(p => p.Id == Id);
            if (announcement == null)
            {
                throw new ArgumentException(string.Format(Constants.AnnouncementDoesNotExist, Id));
            }

            announcement.IsDeleted = true;
            await _db.SaveChangesAsync();
        }

        public async Task<DetailsAnnouncementViewModel> DetailsAnnouncementAsync(int id)
        {
            var result = await _db
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
            var result = await _db
                .Announcements
                .Where(a => a.Id == id && !a.IsDeleted)
                .ProjectTo<EditAnnouncementViewModel>()
                .SingleOrDefaultAsync();

            if (result == null)
            {
                throw new ArgumentException(string.Format(Constants.AnnouncementDoesNotExist, id));
            }

            var announcementAuthor = await _db.Announcements.Where(a => a.Id == id && !a.IsDeleted)
                .Select(r => r.AuthorId).SingleOrDefaultAsync();
            if (announcementAuthor != currentUserId)
            {
                throw new ArgumentException(string.Format(Constants.NotAuthorizedForThisOperation, currentUserId));
            }

            return result;
        }

        public async Task EditAnnouncementAsync(EditAnnouncementViewModel model)
        {
            var announcement = await _db
                .Announcements
                .Where(p => p.Id == model.Id && !p.IsDeleted)
                .SingleOrDefaultAsync();
            if (announcement == null)
            {
                throw new ArgumentException(string.Format(Constants.AnnouncementDoesNotExist, model.Id));
            }

            announcement.Title = model.Title;
            announcement.Content = model.Content;

            await _db.SaveChangesAsync();
        }
    }
}