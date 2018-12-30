namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Statistic;
    using System.Collections.Generic;

    public interface IStatisticService
    {
        StatisticByRating GetStatisticForAllUsersByRating();
    }
}
