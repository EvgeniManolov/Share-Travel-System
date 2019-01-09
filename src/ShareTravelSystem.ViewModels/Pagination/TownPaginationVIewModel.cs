namespace ShareTravelSystem.ViewModels.Pagination
{
    using System.Collections.Generic;
    using Abstract;
    using Town;

    public class TownPaginationViewModel : PaginationViewModel
    {
        public string Search { get; set; }

        public ICollection<DisplayTownViewModel> Towns { get; set; } = new List<DisplayTownViewModel>();
    }
}