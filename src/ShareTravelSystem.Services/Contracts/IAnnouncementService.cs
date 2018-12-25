namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Announcement;
    using System.Collections.Generic;

    public interface IAnnouncementService
    {
        void Create(CreateAnnouncementViewModel model, string userId);

        AnnouncementPaginationViewModel GetAllAnnouncements(bool privateAnnouncements, string search, string currentUserId, int page, int size);

        IEnumerable<DisplayAnnouncementViewModel> GetIndexAnnouncements();

        DetailsAnnouncementViewModel DetailsAnnouncementById(int id);

        EditAnnouncementViewModel EditAnnouncementById(int id);

        void EditAnnouncement(EditAnnouncementViewModel model);

        void Delete(int productId);
    }
}
