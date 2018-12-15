using ShareTravelSystem.Web.Areas.Identity.Data;

namespace ShareTravelSystem.Data.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public int Rate { get; set; }

        public ShareTravelSystemUser  Author { get; set; }

        public string AuthorId { get; set; }
    }
}
