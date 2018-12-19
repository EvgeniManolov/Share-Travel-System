namespace ShareTravelSystem.ViewModels.Announcement
{
    using ShareTravelSystem.Common;
    using System;

    public class DetailsAnnouncementViewModel : IMapFrom<Data.Models.Announcement>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public string Author { get; set; }
    }
}
