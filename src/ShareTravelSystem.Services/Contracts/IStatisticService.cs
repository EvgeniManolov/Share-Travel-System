namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using System.Threading.Tasks;

    public interface IStatisticService
    {
        Task<StatisticByRatingPaginationViewModel> GetStatisticForAllUsersByRatingAsync(int page, string search);
    }
}
