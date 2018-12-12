namespace ShareTravelSystem.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DisplayAllAnnouncementsViewModel
    {
        public IEnumerable<DisplayAnnouncementViewModel> Announcements { get; set; }
    }
}