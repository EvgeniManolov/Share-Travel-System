namespace ShareTravelSystem.ViewModels.Town
{
    using System.ComponentModel.DataAnnotations;

    public class CrateTownViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
