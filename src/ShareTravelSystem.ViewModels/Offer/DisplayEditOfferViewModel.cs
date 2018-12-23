using System;
using System.Collections.Generic;
using System.Text;
using ShareTravelSystem.Data.Models;

namespace ShareTravelSystem.ViewModels.Offer
{
    public class DisplayEditOfferViewModel
    {
        public EditOfferViewModel OfferModel { get; set; }

        public ICollection<Data.Models.Town> Towns { get; set; }
    }
}
