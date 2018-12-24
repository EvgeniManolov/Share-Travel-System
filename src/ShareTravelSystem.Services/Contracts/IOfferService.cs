namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.ViewModels.Offer;
    using System.Collections.Generic;

    public interface IOfferService
    {
        void Create(CreateOfferViewModel model, string userId);

        IEnumerable<Town> GetAllTowns();

        IEnumerable<DisplayOfferViewModel> GetAllOffers(string filter, string currentUserId);

        DetailsOfferViewModel GetOfferById(int id);

        DisplayEditOfferViewModel GetOfferToEdit(int id);
    }
}
