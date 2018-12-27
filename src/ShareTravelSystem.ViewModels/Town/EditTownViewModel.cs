namespace ShareTravelSystem.ViewModels.Town
{
    using System.ComponentModel.DataAnnotations;

    public class EditTownViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
