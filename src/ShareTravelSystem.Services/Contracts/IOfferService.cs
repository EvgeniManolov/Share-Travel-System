﻿namespace ShareTravelSystem.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using ViewModels;
    using ViewModels.Offer;
    using ViewModels.Pagination;

    public interface IOfferService
    {
        Task CreateOfferAsync(CreateOfferViewModel model, string userId);

        IEnumerable<Town> GetAllTowns();

        Task<OfferPaginationViewModel> GetAllOffersAsync(bool privateOffers, string filter, string search,
            string currentUserId, int page);

        Task<DetailsOfferViewModel> DetailsOfferAsync(int id);

        Task<DisplayEditOfferViewModel> GetOfferToEditAsync(int id, string currentUserId);

        Task EditOfferAsync(DisplayEditOfferViewModel model);

        Task<bool> LikeOfferAsync(int id, string userId);

        Task<bool> DisLikeOfferAsync(int id, string userId);

        IEnumerable<int> GetLikedOrDislikedOffersIds(string currentUserId);

        Task DeleteOfferAsync(int id);
    }
}