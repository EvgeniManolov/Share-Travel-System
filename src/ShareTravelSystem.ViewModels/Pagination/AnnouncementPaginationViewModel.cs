namespace ShareTravelSystem.ViewModels.Pagination
{
    using Abstract;
    using Announcement;

    public class AnnouncementPaginationViewModel : PaginationViewModel
    {
        public string Search { get; set; }

        public bool PrivateAnnouncements { get; set; }

        public string TitleOfPage { get; set; }

        public DisplayAllAnnouncementsViewModel AllAnnouncements { get; set; }
    }
}