using ShareTravelSystem.Data.Models;
using ShareTravelSystem.ViewModels;
using ShareTravelSystem.ViewModels.Town;
using System;
using System.Collections.Generic;
using System.Text;
using ShareTravelSystem.ViewModels;

namespace ShareTravelSystem.Services.Contracts
{
    public interface ITownService
    {
        TownPaginationViewModel GetAllTowns(int size, int page);

        void Create(CrateTownViewModel model);

        EditTownViewModel GetTownById(int townId);
    }
}
