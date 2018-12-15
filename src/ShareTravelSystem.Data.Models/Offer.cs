﻿using ShareTravelSystem.Web.Areas.Identity.Data;
using System;

namespace ShareTravelSystem.Data.Models
{
    public class Offer
    {
        public int Id { get; set; }

        public ShareTravelSystemUser Author { get; set; }

        public string AuthorId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArriveDate { get; set; }

        public string Description { get; set; }

        public int Seat { get; set; }

        public OfferType Type { get; set; }

        public int DepartureTownId { get; set; }
        public Town DepartureTown { get; set; }


        public int DestinationTownId { get; set; }
        public Town DestinationTown { get; set; }

        public int TotalRating { get; set; }

        public decimal Price { get; set; }
    }
}
