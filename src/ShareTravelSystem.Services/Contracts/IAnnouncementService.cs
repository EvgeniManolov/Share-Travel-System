namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Announcement;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAnnouncementService
    {
        Task CreateAnnouncementAsync(CreateAnnouncementViewModel model, string userId);

        Task<DetailsAnnouncementViewModel> DetailsAnnouncementAsync(int id);

        Task<EditAnnouncementViewModel> GetAnnouncementToEditAsync(int id, string userId);

        Task EditAnnouncementAsync(EditAnnouncementViewModel model);

        Task DeleteAnnouncementAsync(int Id);

        Task<AnnouncementPaginationViewModel> GetAllAnnouncementsAsync(bool privateAnnouncements, string search, string userId, int page);

        Task<IEnumerable<DisplayAnnouncementViewModel>> GetIndexAnnouncementsAsync();
    }
}
