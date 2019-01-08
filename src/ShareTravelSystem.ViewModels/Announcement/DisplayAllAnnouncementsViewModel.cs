namespace ShareTravelSystem.ViewModels
{
    using System.Collections.Generic;

    public class DisplayAllAnnouncementsViewModel
    {
        public IEnumerable<DisplayAnnouncementViewModel> Announcements { get; set; }
    }
}