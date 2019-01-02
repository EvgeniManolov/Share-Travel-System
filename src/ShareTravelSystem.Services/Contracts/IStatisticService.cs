namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;

    public interface IStatisticService
    {
        StatisticByRatingPaginationViewModel GetStatisticForAllUsersByRating(int page, string search);
    }
}
