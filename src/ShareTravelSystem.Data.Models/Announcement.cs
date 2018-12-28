namespace ShareTravelSystem.Data.Models
{
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Announcement
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public string AuthorId { get; set; }

        public ShareTravelSystemUser Author { get; set; }
    }
}
