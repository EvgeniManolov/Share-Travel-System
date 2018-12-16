
namespace ShareTravelSystem.ViewModels.Offer
{
    using System;


    public class CreateOfferViewModel
    {
        public string Type { get; set; }

        public string DepartureTown { get; set; }

        public string DestinationTown { get; set; }

        public int Seat { get; set; }

        public decimal Price { get; set; }

        public DateTime DepartureDate { get; set; }

        public string Description { get; set; }
    }
}
