namespace ShareTravelSystem.Services
{
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels.Statistic;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class StatisticService : IStatisticService
    {
        private readonly ShareTravelSystemDbContext db;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public StatisticService(ShareTravelSystemDbContext db,
                                    UserManager<ShareTravelSystemUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public StatisticByRating GetStatisticForAllUsersByRating()
        {
            List<DisplayStatisticByUserForRating> statistics = new List<DisplayStatisticByUserForRating>();
            var users = this.db.Users.Select(o => new { id = o.Id, name = o.UserName }).Distinct().ToList();

            foreach (var user in users)
            {
                int totalDisLikes = 0;
                int totalLikes = 0;

                var offers = this.db.Offers.Where(o => o.AuthorId == user.id).Select(o => o.Id).ToList();

                foreach (var offer in offers)
                {
                    int offerLikes = this.db.Reactions.Where(r => r.OfferId == offer && r.Action == true).Count();

                    int offerDisLikes = this.db.Reactions.Where(r => r.OfferId == offer && r.Action == false).Count();

                    totalDisLikes += offerDisLikes;
                    totalLikes += offerLikes;
                }

                DisplayStatisticByUserForRating statisticByUser = new DisplayStatisticByUserForRating
                {
                    UserId = user.id,
                    UserName = user.name,
                    TotalLikes = totalLikes,
                    TotalDisLikes = totalDisLikes,
                    TotalRating = totalLikes - totalDisLikes
                };

                statistics.Add(statisticByUser);
            }

            StatisticByRating result = new StatisticByRating { Statistics = statistics.OrderByDescending(x => x.TotalRating).ToList() };

            return result;
        }
    }
}
