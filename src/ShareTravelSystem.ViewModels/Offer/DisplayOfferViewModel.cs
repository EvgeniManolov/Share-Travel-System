
namespace ShareTravelSystem.ViewModels.Offer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DisplayOfferViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "DepartureDate")]
        public DateTime DepartureDate { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Seat")]
        public int Seat { get; set; }

        [Required]
        [Display(Name = "DepartureTown")]
        public string DepartureTown { get; set; }

        [Required]
        [Display(Name = "DestinationTown")]
        public string DestinationTown { get; set; }
        
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
