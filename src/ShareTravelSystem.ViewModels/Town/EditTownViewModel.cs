namespace ShareTravelSystem.ViewModels.Town
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;

    public class EditTownViewModel: IMapFrom<Town>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
