namespace ShareTravelSystem.Data.Models
{
    using System;

    public class Announcement
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public ShareTravelSystemUser Author { get; set; }

        public string AuthorId { get; set; }
    }
}
