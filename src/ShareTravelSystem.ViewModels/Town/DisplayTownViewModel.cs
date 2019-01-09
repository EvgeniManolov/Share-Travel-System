namespace ShareTravelSystem.ViewModels.Town
{
    using Common;
    using Data.Models;

    public class DisplayTownViewModel : IMapFrom<Town>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}