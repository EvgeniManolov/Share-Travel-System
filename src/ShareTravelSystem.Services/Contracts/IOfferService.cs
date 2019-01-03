namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Offer;
    using System.Collections.Generic;

    public interface IOfferService
    {
        void CreateOffer(CreateOfferViewModel model, string userId);

        IEnumerable<Town> GetAllTowns();

        OfferPaginationViewModel GetAllOffers(bool privateOffers, string filter, string search, string currentUserId, int page, int size);

        DetailsOfferViewModel DetailsOffer(int id);

        DisplayEditOfferViewModel GetOfferToEdit(int id, string currentUserId);

        void EditOffer(DisplayEditOfferViewModel model);

        bool LikeOffer(int offerId, string userId);

        bool DisLikeOffer(int offerId, string userId);

        ICollection<int> GetLikedOrDislikedOffersIds(string currentUserId);

        void DeleteOffer(int id);
    }
}
