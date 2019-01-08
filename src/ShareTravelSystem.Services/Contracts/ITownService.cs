namespace ShareTravelSystem.Services.Contracts
{
    using System.Threading.Tasks;
    using ViewModels;
    using ViewModels.Town;

    public interface ITownService
    {
        Task<TownPaginationViewModel> GetAllTownsAsync(int page, string search);

        Task CreateTownAsync(CrateTownViewModel model);

        Task<EditTownViewModel> GetTownToEditAsync(int townId);

        Task DeleteTownAsync(int townId);

        Task EditTownAsync(EditTownViewModel model);
    }
}
