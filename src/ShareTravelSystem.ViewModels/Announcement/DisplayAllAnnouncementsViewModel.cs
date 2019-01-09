namespace ShareTravelSystem.ViewModels.Announcement
{
    using System.Collections.Generic;

    public class DisplayAllAnnouncementsViewModel
    {
        public IEnumerable<DisplayAnnouncementViewModel> Announcements { get; set; }
    }
}