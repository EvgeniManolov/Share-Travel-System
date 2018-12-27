namespace ShareTravelSystem.ViewModels
{
    using ShareTravelSystem.ViewModels.Town;
    using System.Collections.Generic;

    public class TownPaginationViewModel : PaginationViewModel
    {
        public string Search { get; set; }

        public ICollection<DisplayTownViewModel> Towns { get; set; } = new List<DisplayTownViewModel>();
    }
}
