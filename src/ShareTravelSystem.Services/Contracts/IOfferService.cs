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

        OfferPaginationViewModel GetAllOffers(bool privateOffers, string filter, string search, string currentUserId, int page, int size);

        DetailsOfferViewModel GetOfferById(int id);

        DisplayEditOfferViewModel GetOfferToEdit(int id);

        void EditOffer(DisplayEditOfferViewModel model);

        bool AddLikeToOffer(int offerId, string userId);

        bool AddDisLikeToOffer(int offerId, string userId);

        List<int> GetLikedOrDislikedOffersIds(string currentUserId);
    }
}
