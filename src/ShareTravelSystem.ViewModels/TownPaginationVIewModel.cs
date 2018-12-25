using ShareTravelSystem.ViewModels.Town;
using System.Collections.Generic;

namespace ShareTravelSystem.ViewModels
{
    public class TownPaginationViewModel : PaginationViewModel
    {
        public ICollection<DisplayTownViewModel> Towns { get; set; } = new List<DisplayTownViewModel>();
    }
}
