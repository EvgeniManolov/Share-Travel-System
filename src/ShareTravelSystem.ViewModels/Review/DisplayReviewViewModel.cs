namespace ShareTravelSystem.ViewModels.Review
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DisplayReviewViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
