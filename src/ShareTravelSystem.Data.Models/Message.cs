namespace ShareTravelSystem.Data.Models
{
    using System;
    using Web.Areas.Identity.Data;

    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string AuthorId { get; set; }

        public ShareTravelSystemUser Author { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
