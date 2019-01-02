namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Town;


    public interface ITownService
    {
        TownPaginationViewModel GetAllTowns(int page, string search);

        void Create(CrateTownViewModel model);

        EditTownViewModel GetTownById(int townId);

        void Delete(int townId);

        void EditTownById(EditTownViewModel model);
    }
}
