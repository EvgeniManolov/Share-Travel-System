namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Town;
    using System.Threading.Tasks;

    public interface ITownService
    {
        Task<TownPaginationViewModel> GetAllTowns(int page, string search);

        Task CreateTown(CrateTownViewModel model);

        Task<EditTownViewModel> GetTownToEdit(int townId);

        Task DeleteTown(int townId);

        Task EditTown(EditTownViewModel model);
    }
}
