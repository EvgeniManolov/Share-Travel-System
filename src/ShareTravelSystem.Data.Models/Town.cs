using System.Collections.Generic;

namespace ShareTravelSystem.Data.Models
{
    public class Town
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public ICollection<Offer> TownsAsDepartureTown { get; set; } = new List<Offer>();


        public ICollection<Offer> TownsAsDestinationTown { get; set; } = new List<Offer>();
    }
}
