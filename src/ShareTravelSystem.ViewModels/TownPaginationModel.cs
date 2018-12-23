using ShareTravelSystem.ViewModels.Town;
using System.Collections.Generic;

namespace ShareTravelSystem.ViewModels
{
    public class TownPaginationModel : PaginationModel
    {
        public ICollection<DisplayTownViewModel> Towns { get; set; } = new List<DisplayTownViewModel>();
    }
}
