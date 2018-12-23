using ShareTravelSystem.Data.Models;
using ShareTravelSystem.ViewModels;
using ShareTravelSystem.ViewModels.Town;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareTravelSystem.Services.Contracts
{
    public interface ITownService
    {
        TownPaginationModel GetAllTowns(int size, int page);

        void Create(CrateTownViewModel model);

        EditTownViewModel GetTownById(int townId);
    }
}
