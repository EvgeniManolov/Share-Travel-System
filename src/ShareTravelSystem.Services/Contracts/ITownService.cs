namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Town;
    using System.Threading.Tasks;

    public interface ITownService
    {
        Task<TownPaginationViewModel> GetAllTownsAsync(int page, string search);

        Task CreateTownAsync(CrateTownViewModel model);

        Task<EditTownViewModel> GetTownToEditAsync(int townId);

        Task DeleteTownAsync(int townId);

        Task EditTownAsync(EditTownViewModel model);
    }
}
