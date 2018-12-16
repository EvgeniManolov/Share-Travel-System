namespace ShareTravelSystem.Data.Models
{
    using ShareTravelSystem.Web.Areas.Identity.Data;

    public class Review
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public int Rate { get; set; }

        public ShareTravelSystemUser  Author { get; set; }

        public string AuthorId { get; set; } 

        public int OfferId { get; set; }

        public Offer Offer { get; set; }
    }
}
