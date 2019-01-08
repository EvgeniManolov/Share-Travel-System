namespace ShareTravelSystem.ViewModels
{
    using Statistic;

    public class StatisticByRatingPaginationViewModel : PaginationViewModel
    {
        public string Search { get; set; }

        public StatisticByRating Statistic { get; set; }
    }
}
