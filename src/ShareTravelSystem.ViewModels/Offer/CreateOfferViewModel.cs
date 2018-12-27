namespace ShareTravelSystem.ViewModels.Offer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateOfferViewModel
    {
        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "DepartureName")]
        public int DepartureTownId { get; set; }

        [Required]
        [Display(Name = "DestinationName")]
        public int DestinationTownId { get; set; }

        [Required]
        [Display(Name = "Seat")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a seat bigger or equal from {1}")]
        public int Seat { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a price bigger or equal from {1}")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "DepartureDate")]
        public DateTime DepartureDate { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Description { get; set; }
        
    }
}
    