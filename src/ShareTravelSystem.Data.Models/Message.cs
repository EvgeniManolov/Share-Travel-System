namespace ShareTravelSystem.Data.Models
{
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using System;

    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string AuthorId { get; set; }

        public ShareTravelSystemUser Author { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
