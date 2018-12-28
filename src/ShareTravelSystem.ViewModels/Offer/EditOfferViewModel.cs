namespace ShareTravelSystem.ViewModels.Offer
{
    using ShareTravelSystem.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditOfferViewModel : IMapFrom<Data.Models.Offer>
    {
        [Required]
        [Display(Name = "Type")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Departure Town")]
        public string DepartureTownName { get; set; }

        [Required]
        [Display(Name = "Departure Town")]
        public int DepartureTownId { get; set; }

        [Required]
        [Display(Name = "Destination Town")]
        public string DestinationTownName { get; set; }

        [Display(Name = "Destination Town")]
        public int DestinationTownId { get; set; }

        [Required]
        [Display(Name = "Seat")]
        [Range(1, int.MaxValue, ErrorMessage = "Seat must greater or equal to one.")]
        public int Seat { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must greater or equal to zero.")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Departure Date")]
        public DateTime DepartureDate { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
