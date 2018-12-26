namespace ShareTravelSystem.ViewModels.Offer
{
    using ShareTravelSystem.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditOfferViewModel : IMapFrom<Data.Models.Offer>
    {
        public string Type { get; set; }

        [Display(Name = "Departure Town")]
        public string DepartureTownName { get; set; }
        [Display(Name = "Departure Town")]
        public int DepartureTownId { get; set; }


        [Display(Name = "Destination Town")]
        public string DestinationTownName { get; set; }
        [Display(Name = "Destination Town")]
        public int DestinationTownId { get; set; }

        public int Seat { get; set; }

        public decimal Price { get; set; }

        public DateTime DepartureDate { get; set; }

        public string Description { get; set; }
    }
}
