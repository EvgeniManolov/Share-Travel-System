namespace ShareTravelSystem.Web.Areas.Identity.Data
{
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Data.Models;
    using System.Collections.Generic;

    // Add profile data for application users by adding properties to the ShareTravelSystemUser class
    public class ShareTravelSystemUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}
