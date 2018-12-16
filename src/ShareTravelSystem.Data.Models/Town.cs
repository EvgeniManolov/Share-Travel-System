using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareTravelSystem.Data.Models
{
    [Table("Towns")]
    public class Town
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public ICollection<Offer> TownsAsDepartureTown { get; set; } = new List<Offer>();


        public ICollection<Offer> TownsAsDestinationTown { get; set; } = new List<Offer>();
    }
}
