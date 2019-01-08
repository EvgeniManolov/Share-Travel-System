namespace ShareTravelSystem.Data.Models
{
    using System;
    using Web.Areas.Identity.Data;

    public class Announcement
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public string AuthorId { get; set; }

        public ShareTravelSystemUser Author { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
