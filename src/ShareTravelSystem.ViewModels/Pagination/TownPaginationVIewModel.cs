namespace ShareTravelSystem.ViewModels
{
    using System.Collections.Generic;
    using Town;

    public class TownPaginationViewModel : PaginationViewModel
    {
        public string Search { get; set; }

        public ICollection<DisplayTownViewModel> Towns { get; set; } = new List<DisplayTownViewModel>();
    }
}
