namespace ShareTravelSystem.Data.Models
{
    using Web.Areas.Identity.Data;

    public class Reaction
    {
        public int Id { get; set; }

        public bool Action { get; set; }

        public int OfferId { get; set; }

        public Offer Offer { get; set; }

        public string AuthorId { get; set; }

        public ShareTravelSystemUser Author { get; set; }
    }
}
