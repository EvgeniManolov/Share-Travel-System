namespace ShareTravelSystem.ViewModels.Announcement
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class EditAnnouncementViewModel : IMapFrom<Data.Models.Announcement>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        [StringLength(400, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 4)]
        public string Content { get; set; }
    }
}