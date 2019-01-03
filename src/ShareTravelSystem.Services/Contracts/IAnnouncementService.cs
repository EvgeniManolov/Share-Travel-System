namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Announcement;
    using System.Collections.Generic;

    public interface IAnnouncementService
    {
        void CreateAnnouncement(CreateAnnouncementViewModel model, string userId);

        DetailsAnnouncementViewModel DetailsAnnouncement(int id);

        EditAnnouncementViewModel GetAnnouncementToEdit(int id, string userId);

        void EditAnnouncement(EditAnnouncementViewModel model);

        void DeleteAnnouncement(int productId);

        AnnouncementPaginationViewModel GetAllAnnouncements(bool privateAnnouncements, string search, string currentUserId, int page);

        IEnumerable<DisplayAnnouncementViewModel> GetIndexAnnouncements();
    }
}
