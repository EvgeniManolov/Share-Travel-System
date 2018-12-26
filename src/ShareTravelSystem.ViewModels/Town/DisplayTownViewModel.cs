namespace ShareTravelSystem.ViewModels.Town
{
    using ShareTravelSystem.Common;

    public class DisplayTownViewModel : IMapFrom<Data.Models.Town>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
