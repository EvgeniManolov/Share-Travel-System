namespace ShareTravelSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Towns")]
    public class Town
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Offer> TownsAsDepartureTown { get; set; } = new List<Offer>();

        public ICollection<Offer> TownsAsDestinationTown { get; set; } = new List<Offer>();
    }
}