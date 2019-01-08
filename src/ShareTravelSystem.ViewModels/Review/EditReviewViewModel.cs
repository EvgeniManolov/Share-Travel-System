namespace ShareTravelSystem.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;

    public class EditReviewViewModel: IMapFrom<Review>
    {
        public int Id { get; set; }

        public int OfferId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Comment { get; set; }
    }
}
