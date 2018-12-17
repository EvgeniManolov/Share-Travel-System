namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.ViewModels.Offer;
    using System.Collections.Generic;

    public interface IOfferService
    {
        void Create(CreateOfferViewModel model, string userId);

        List<Town> GetAllTowns();
    }
}
