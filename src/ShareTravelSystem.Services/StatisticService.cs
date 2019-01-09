namespace ShareTravelSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;
    using ViewModels.Statistic;
    using Web.Models;

    public class StatisticService : IStatisticService
    {
        private readonly ShareTravelSystemDbContext _db;

        public StatisticService(ShareTravelSystemDbContext db)
        {
            this._db = db;
        }

        public async Task<StatisticByRatingPaginationViewModel> GetStatisticForAllUsersByRatingAsync(int page,
            string search)
        {
            var size = Constants.UserStatisticsPerPage;
            if (page == 0) page = 1;
            var statistics = new List<DisplayStatisticByUserForRating>();

            var users = await _db.Users.Select(o => new {id = o.Id, name = o.UserName}).Distinct().ToListAsync();

            if (search != null)
            {
                users = await _db.Users.Where(u => u.Email.ToLower().Contains(search.ToLower()))
                    .Select(o => new {id = o.Id, name = o.UserName}).Distinct().ToListAsync();
            }


            foreach (var user in users)
            {
                var totalDisLikes = 0;
                var totalLikes = 0;

                var offersIds = await _db.Offers.Where(o => o.AuthorId == user.id && !o.IsDeleted)
                    .Select(o => o.Id).ToListAsync();

                foreach (var offerId in offersIds)
                {
                    var offerLikes = _db.Reactions.Count(r => r.OfferId == offerId && r.Action);

                    var offerDisLikes = _db.Reactions.Count(r => r.OfferId == offerId && r.Action == false);

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