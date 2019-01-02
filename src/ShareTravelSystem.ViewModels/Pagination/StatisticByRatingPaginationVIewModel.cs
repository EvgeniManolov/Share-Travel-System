namespace ShareTravelSystem.ViewModels
{
    using ShareTravelSystem.ViewModels.Statistic;

    public class StatisticByRatingPaginationViewModel : PaginationViewModel
    {
        public string Search { get; set; }

        public StatisticByRating Statistic { get; set; }
    }
}
