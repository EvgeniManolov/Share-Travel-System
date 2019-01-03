namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Town;


    public interface ITownService
    {
        TownPaginationViewModel GetAllTowns(int page, string search);

        void CreateTown(CrateTownViewModel model);

        EditTownViewModel GetTownToEdit(int townId);

        void DeleteTown(int townId);

        void EditTown(EditTownViewModel model);
    }
}
