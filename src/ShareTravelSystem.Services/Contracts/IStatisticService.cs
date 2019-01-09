namespace ShareTravelSystem.Services.Contracts
{
    using System.Threading.Tasks;
    using ViewModels;
    using ViewModels.Pagination;

    public interface IStatisticService
    {
        Task<StatisticByRatingPaginationViewModel> GetStatisticForAllUsersByRatingAsync(int page, string search);
    }
}