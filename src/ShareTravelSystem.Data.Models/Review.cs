﻿namespace ShareTravelSystem.Data.Models
{
    using System;
    using Web.Areas.Identity.Data;

    public class Review
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public string AuthorId { get; set; }

        public ShareTravelSystemUser Author { get; set; }

        public int OfferId { get; set; }

        public Offer Offer { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}