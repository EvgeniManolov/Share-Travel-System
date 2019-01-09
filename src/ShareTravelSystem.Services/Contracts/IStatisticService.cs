namespace ShareTravelSystem.Services.Contracts
{
    using System.Threading.Tasks;
    using ViewModels;

    public interface IStatisticService
    {
        Task<StatisticByRatingPaginationViewModel> GetStatisticForAllUsersByRatingAsync(int page, string search);
    }
}