namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using System.Collections.Generic;

    public interface IAnnouncementService
    {
        void Create(CreateAnnouncementViewModel model, string userid);

        List<DisplayAnnouncementViewModel> GetAllAnnouncements();
    }
}
