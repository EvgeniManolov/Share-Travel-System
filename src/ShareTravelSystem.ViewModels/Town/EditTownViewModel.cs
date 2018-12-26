namespace ShareTravelSystem.ViewModels.Town
{
    using System.ComponentModel.DataAnnotations;

    public class EditTownViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
