namespace ShareTravelSystem.ViewModels.Offer
{
    using System.Collections.Generic;

    public class DisplayEditOfferViewModel
    {
        public EditOfferViewModel OfferModel { get; set; }

        public ICollection<Data.Models.Town> Towns { get; set; }
    }
}
