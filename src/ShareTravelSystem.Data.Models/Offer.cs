namespace ShareTravelSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Web.Areas.Identity.Data;

    public class Offer
    {
        public int Id { get; set; }

        public ShareTravelSystemUser Author { get; set; }

        public string AuthorId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public string Description { get; set; }

        public int Seat { get; set; }

        public OfferType Type { get; set; }

        public int DepartureTownId { get; set; }

        public Town DepartureTown { get; set; }

        public int DestinationTownId { get; set; }

        public Town DestinationTown { get; set; }

        public int TotalRating { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();
    }
}