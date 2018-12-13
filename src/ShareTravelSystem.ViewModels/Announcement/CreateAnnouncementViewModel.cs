namespace ShareTravelSystem.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateAnnouncementViewModel
    {

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}