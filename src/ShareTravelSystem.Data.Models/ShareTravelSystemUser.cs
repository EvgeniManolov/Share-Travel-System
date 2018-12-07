namespace ShareTravelSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class ShareTravelSystemUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}
