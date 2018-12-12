namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;

    public interface IAnnouncementService
    {
        void Create(CreateAnnouncementViewModel model, string userid);
    }
}
