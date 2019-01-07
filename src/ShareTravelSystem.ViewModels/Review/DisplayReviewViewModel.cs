namespace ShareTravelSystem.ViewModels.Review
{
    using ShareTravelSystem.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DisplayReviewViewModel: IMapFrom<Data.Models.Review>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Comment { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
}
