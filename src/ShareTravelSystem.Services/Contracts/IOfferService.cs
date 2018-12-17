namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels.Offer;

    public interface IOfferService
    {
        void Create(CreateOfferViewModel model, string userId);
    }
}
