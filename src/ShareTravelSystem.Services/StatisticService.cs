namespace ShareTravelSystem.Services
{
    using Microsoft.EntityFrameworkCore;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Services.Infrastructure;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.ViewModels.Statistic;
    using ShareTravelSystem.Web.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class StatisticService : IStatisticService
    {
        private readonly ShareTravelSystemDbContext db;

        public StatisticService(ShareTravelSystemDbContext db)
        {
            this.db = db;
        }
        public async Task<StatisticByRatingPaginationViewModel> GetStatisticForAllUsersByRatingAsync(int page, string search)
        {

            int size = Constants.UserStatisticsPerPage;
            if (page == 0) page = 1;
            List<DisplayStatisticByUserForRating> statistics = new List<DisplayStatisticByUserForRating>();

            var users = await this.db.Users.Select(o => new { id = o.Id, name = o.UserName }).Distinct().ToListAsync();

            if (search != null)
            {
                users = await this.db.Users.Where(u => u.Email.ToLower().Contains(search.ToLower())).Select(o => new { id = o.Id, name = o.UserName }).Distinct().ToListAsync();
            }


            foreach (var user in users)
            {
                int totalDisLikes = 0;
                int totalLikes = 0;

                List<int> offersIds = await this.db.Offers.Where(o => o.AuthorId == user.id && !o.IsDeleted).Select(o => o.Id).ToListAsync();

                foreach (var offerId in offersIds)
                {
                    int offerLikes = this.db.Reactions.Where(r => r.OfferId == offerId && r.Action == true).Count();

                    int offerDisLikes = this.db.Reactions.Where(r => r.OfferId == offerId && r.Action == false).Count();

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

            int count = statistics.Count();
            statistics = statistics.Skip((page - 1) * size).Take(size).ToList();

            StatisticByRating statistic = new StatisticByRating { Statistics = statistics.OrderByDescending(x => x.TotalRating).ToList()};
            StatisticByRatingPaginationViewModel result = new StatisticByRatingPaginationViewModel
                                    { Search = search,
                                      Size = size,
                                      Page = page,
                                      Count = count,
                                      Statistic = statistic };

            return result;
        }
    }
}
