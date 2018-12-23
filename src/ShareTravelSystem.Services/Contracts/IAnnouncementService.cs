namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Announcement;
    using System.Collections.Generic;

    public interface IAnnouncementService
    {
        void Create(CreateAnnouncementViewModel model, string userId);

        IEnumerable<DisplayAnnouncementViewModel> GetAllAnnouncements();

        IEnumerable<DisplayAnnouncementViewModel> GetMyAnnouncements(string userId);

        IEnumerable<DisplayAnnouncementViewModel> GetIndexAnnouncements();

        void Delete(int productId);

        DetailsAnnouncementViewModel DetailsAnnouncementById(int id);
        EditAnnouncementViewModel EditAnnouncementById(int id);
        void EditAnnouncement(EditAnnouncementViewModel model);
    }
}
