namespace ShareTravelSystem.Web.Areas.Identity.Data
{
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Data.Models;
    using System;
    using System.Collections.Generic;
    
    public class ShareTravelSystemUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

        public ICollection<Offer> Offers { get; set; } = new List<Offer>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
