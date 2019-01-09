namespace ShareTravelSystem.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;
    using ViewModels.Announcement;

    public interface IAnnouncementService
    {
        Task CreateAnnouncementAsync(CreateAnnouncementViewModel model, string userId);

        Task<DetailsAnnouncementViewModel> DetailsAnnouncementAsync(int id);

        Task<EditAnnouncementViewModel> GetAnnouncementToEditAsync(int id, string userId);

        Task EditAnnouncementAsync(EditAnnouncementViewModel model);

        Task DeleteAnnouncementAsync(int Id);

        Task<AnnouncementPaginationViewModel> GetAllAnnouncementsAsync(bool privateAnnouncements, string search,
            string userId, int page);

        Task<IEnumerable<DisplayAnnouncementViewModel>> GetIndexAnnouncementsAsync();
    }
}