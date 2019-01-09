namespace ShareTravelSystem.ViewModels.Announcement
{
    using System;
    using Common;
    using Data.Models;

    public class DetailsAnnouncementViewModel : IMapFrom<Announcement>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public string Author { get; set; }
    }
}