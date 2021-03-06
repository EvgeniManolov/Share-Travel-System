﻿namespace ShareTravelSystem.ViewModels.Pagination
{
    using Abstract;
    using Offer;

    public class OfferPaginationViewModel : PaginationViewModel
    {
        public string Filter { get; set; }

        public string Search { get; set; }

        public bool PrivateOffers { get; set; }

        public string TitleOfPage { get; set; }

        public DisplayAllOffersViewModel AllOffers { get; set; }
    }
}