
namespace ShareTravelSystem.ViewModels.Offer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;
    using Review;

    public class DetailsOfferViewModel : IMapFrom<Offer>
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
        [Display(Name = "CreateDate")]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Seat")]
        public int Seat { get; set; }

        [Required]
        [Display(Name = "DepartureTownName")]
        public string DepartureTownName { get; set; }

        [Required]
        [Display(Name = "DestinationTownName")]
        public string DestinationTownName { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }


        [Required]
        [Display(Name = "TotalRating")]
        public int TotalRating { get; set; }

        public ICollection<DisplayReviewViewModel> Reviews { get; set; }
    }
}
