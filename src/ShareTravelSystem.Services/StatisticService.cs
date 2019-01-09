namespace ShareTravelSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;
    using ViewModels.Pagination;
    using ViewModels.Statistic;

    public class StatisticService : IStatisticService
    {
        private readonly ShareTravelSystemDbContext db;

        public StatisticService(ShareTravelSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<StatisticByRatingPaginationViewModel> GetStatisticForAllUsersByRatingAsync(int page,
            string search)
        {
            const int size = Constants.UserStatisticsPerPage;
            if (page == 0) page = 1;
            var statistics = new List<DisplayStatisticByUserForRating>();

            var users = await this.db.Users.Select(o => new {id = o.Id, name = o.UserName}).Distinct().ToListAsync();

            if (search != null)
            {
                users = await this.db.Users.Where(u => u.Email.ToLower().Contains(search.ToLower()))
                    .Select(o => new {id = o.Id, name = o.UserName}).Distinct().ToListAsync();
            }


            foreach (var user in users)
            {
                var totalDisLikes = 0;
                var totalLikes = 0;

                var offersIds = await this.db.Offers.Where(o => o.AuthorId == user.id && !o.IsDeleted)
                    .Select(o => o.Id).ToListAsync();

                foreach (var offerId in offersIds)
                {
                    var offerLikes = this.db.Reactions.Count(r => r.OfferId == offerId && r.Action);

                    var offerDisLikes = this.db.Reactions.Count(r => r.OfferId == offerId && r.Action == false);

                    totalDisLikes += offerDisLikes;
                    totalLikes += offerLikes;
                }

                var statisticByUser = new DisplayStatisticByUserForRating
                {
                    UserId = user.id,
                    UserName = user.name,
                    TotalLikes = totalLikes,
                    TotalDisLikes = totalDisLikes,
                    TotalRating = totalLikes - totalDisLikes
                };

                statistics.Add(statisticByUser);
            }

            var count = statistics.Count();
            statistics = statistics.Skip((page - 1) * size).Take(size).ToList();

            var statistic =
                new StatisticByRating {Statistics = statistics.OrderByDescending(x => x.TotalRating).ToList()};
            var result = new StatisticByRatingPaginationViewModel
            {
                Search = search,
                Size = size,
                Page = page,
                Count = count,
                Statistic = statistic
            };

            return result;
        }
    }
}