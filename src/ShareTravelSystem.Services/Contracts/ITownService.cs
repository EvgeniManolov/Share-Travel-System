using ShareTravelSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareTravelSystem.Services.Contracts
{
    public interface ITownService
    {
        List<Town> GetAllTowns();
    }
}
