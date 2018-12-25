using ShareTravelSystem.ViewModels;
using ShareTravelSystem.ViewModels.Town;

namespace ShareTravelSystem.Services.Contracts
{
    public interface ITownService
    {
        TownPaginationViewModel GetAllTowns(int size, int page);

        void Create(CrateTownViewModel model);

        EditTownViewModel GetTownById(int townId);
    }
}
