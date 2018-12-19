namespace ShareTravelSystem.ViewModels
{
    using ShareTravelSystem.Common;
    using System.ComponentModel.DataAnnotations;

    public class EditAnnouncementViewModel : IMapFrom<Data.Models.Announcement>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}