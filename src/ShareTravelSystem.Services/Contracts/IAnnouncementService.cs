namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using System.Collections.Generic;

    public interface IAnnouncementService
    {
        void Create(CreateAnnouncementViewModel model, string userId);

        List<DisplayAnnouncementViewModel> GetAllAnnouncements();

        List<DisplayAnnouncementViewModel> GetMyAnnouncements(string userId);

        List<DisplayAnnouncementViewModel> GetIndexAnnouncements();

        void Delete(int productId);
    }
}
