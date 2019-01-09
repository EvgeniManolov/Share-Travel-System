namespace ShareTravelSystem.ViewModels.Offer
{
    using System.Collections.Generic;
    using Data.Models;

    public class DisplayEditOfferViewModel
    {
        public EditOfferViewModel OfferModel { get; set; }

        public ICollection<Town> Towns { get; set; }
    }
}