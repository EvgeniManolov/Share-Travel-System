namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Offer;
    using System.Collections.Generic;

    public interface IOfferService
    {
        void Create(CreateOfferViewModel model, string userId);

        IEnumerable<Town> GetAllTowns();

        OfferPaginationViewModel GetAllOffers(bool privateOffers ,string filter, string search, string currentUserId);

        DetailsOfferViewModel GetOfferById(int id);

        DisplayEditOfferViewModel GetOfferToEdit(int id);
    }
}
