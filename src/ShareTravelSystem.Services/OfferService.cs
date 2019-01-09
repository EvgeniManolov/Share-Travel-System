namespace ShareTravelSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;
    using ViewModels.Offer;
    using ViewModels.Review;
    using Web.Areas.Identity.Data;
    using Web.Models;

    public class OfferService : IOfferService
    {
        private readonly ShareTravelSystemDbContext _db;
        private readonly UserManager<ShareTravelSystemUser> _userManager;

        public OfferService(ShareTravelSystemDbContext db, UserManager<ShareTravelSystemUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        public async Task CreateOfferAsync(CreateOfferViewModel model, string userId)
        {
            if (!Enum.TryParse(model.Type, true, out OfferType type))
            {
                throw new ArgumentException(string.Format(Constants.OfferTypeDoesNotExist, model.Type));
            }

            var departureTownId = await _db
                .Towns
                .Where(t => t.Id == model.DepartureTownId && !t.IsDeleted)
                .Select(x => x.Id)
                .SingleOrDefaultAsync();

            var destinationTownId = await _db
                .Towns
                .Where(t => t.Id == model.DestinationTownId && !t.IsDeleted)
                .Select(x => x.Id)
                .SingleOrDefaultAsync();

            _db.Offers.Add(new Offer
            {
                AuthorId = userId,
                CreateDate = DateTime.UtcNow,
                DepartureDate = model.DepartureDate,
                Description = model.Description,
                Seat = model.Seat,
                Type = type,
                DepartureTownId = departureTownId,
                DestinationTownId = destinationTownId,
                TotalRating = 0,
                Price = model.Price
            });

            await _db.SaveChangesAsync();
        }

        public async Task<DetailsOfferViewModel> DetailsOfferAsync(int id)
        {
            var offer = await _db.Offers
                .Where(t => t.Id == id && !t.IsDeleted)
                .ProjectTo<DetailsOfferViewModel>()
                .SingleOrDefaultAsync();
            if (offer == null)
            {
                throw new ArgumentException(string.Format(Constants.OfferDoesNotExist, id));
            }

            offer.Reviews = await _db
                .Reviews
                .Where(r => r.OfferId == id && !r.IsDeleted)
                .OrderByDescending(x => x.CreateDate)
                .ProjectTo<DisplayReviewViewModel>().ToListAsync();

            return offer;
        }

        public async Task<OfferPaginationViewModel> GetAllOffersAsync(bool privateOffers, string filter, string search,
            string currentUserId, int page)
        {
            var offers = new List<DisplayOfferViewModel>();
            var titleOfPage = "";
            var size = Constants.OffersPerPage;
            if (page == 0) page = 1;

            if (!privateOffers)
            {
                offers = await _db.Offers
                    .Where(o => !o.IsDeleted)
                    .OrderByDescending(x => x.CreateDate)
                    .ProjectTo<DisplayOfferViewModel>()
                    .ToListAsync();

                titleOfPage = Constants.AllOffersTitlePageName;
            }
            else
            {
                offers = await _db.Offers
                    .Where(o => o.AuthorId == currentUserId && !o.IsDeleted)
                    .OrderByDescending(x => x.CreateDate)
                    .ProjectTo<DisplayOfferViewModel>()
                    .ToListAsync();

                titleOfPage = Constants.MyOffersTitlePageName;
            }

            if (!string.IsNullOrEmpty(filter) && filter != Constants.FilterOfAllOffers)
            {
                offers = offers
                    .Where(x => x.Type.ToLower() == filter.ToLower())
                    .ToList();
            }


            if (!string.IsNullOrEmpty(search))
            {
                offers = offers.Where(x =>
                        x.DepartureTownName.ToLower().Contains(search.Trim().ToLower()) || x.DestinationTownName
                                                                                            .ToLower().Contains(search
                                                                                                .Trim().ToLower())
                                                                                        || x.Description.ToLower()
                                                                                            .Contains(search.Trim()
                                                                                                .ToLower()))
                    .ToList();
            }

            var count = offers.Count();
            offers = offers.Skip((page - 1) * size).Take(size).ToList();

            var result = new OfferPaginationViewModel
            {
                Filter = filter,
                Search = search,
                Size = size,
                Page = page,
                Count = count,
                TitleOfPage = titleOfPage,
                PrivateOffers = privateOffers,
                AllOffers = new DisplayAllOffersViewModel
                {
                    Offers = offers
                }
            };

            return result;
        }

        public IEnumerable<Town> GetAllTowns()
        {
            return _db.Towns.Where(t => !t.IsDeleted).ToList();
        }

        public async Task<DisplayEditOfferViewModel> GetOfferToEditAsync(int id, string currentUserId)
        {
            var model = await _db
                .Offers
                .Where(o => o.Id == id && !o.IsDeleted)
                .ProjectTo<EditOfferViewModel>()
                .SingleOrDefaultAsync();
            if (model == null)
            {
                throw new ArgumentException(string.Format(Constants.OfferDoesNotExist, model.Id));
            }

            var offerAuthor = await _db.Offers.Where(o => o.Id == id && !o.IsDeleted).Select(x => x.AuthorId)
                .SingleOrDefaultAsync();
            if (offerAuthor != currentUserId)
            {
                throw new ArgumentException(string.Format(Constants.NotAuthorizedForThisOperation, currentUserId));
            }

            var result = new DisplayEditOfferViewModel
            {
                OfferModel = model,
                Towns = _db.Towns.Where(t => !t.IsDeleted).ToList()
            };

            return result;
        }

        public async Task EditOfferAsync(DisplayEditOfferViewModel model)
        {
            var offer = await _db.Offers.Where(o => o.Id == model.OfferModel.Id && !o.IsDeleted)
                .SingleOrDefaultAsync();

            if (!Enum.TryParse(model.OfferModel.Type, true, out OfferType type))
            {
                throw new ArgumentException(string.Format(Constants.OfferTypeDoesNotExist, model.OfferModel.Type));
            }

            offer.Type = type;
            offer.DepartureTownId = model.OfferModel.DepartureTownId;
            offer.DestinationTownId = model.OfferModel.DestinationTownId;
            offer.Seat = model.OfferModel.Seat;
            offer.Price = model.OfferModel.Price;
            offer.DepartureDate = model.OfferModel.DepartureDate;
            offer.Description = model.OfferModel.Description;

            await _db.SaveChangesAsync();
        }

        public async Task<bool> LikeOfferAsync(int offerId, string userId)
        {
            var isExist = await _db.Reactions.Where(r => r.AuthorId == userId && r.OfferId == offerId)
                .SingleOrDefaultAsync();

            if (isExist != null)
            {
                throw new ArgumentException(string.Format(Constants.AlreadyTakeActionToThisOffer, offerId));
            }

            var reation = new Reaction
            {
                Action = true,
                AuthorId = userId,
                OfferId = offerId
            };

            _db.Reactions.Add(reation);

            var likedOffer = await _db.Offers.Where(o => o.Id == offerId && !o.IsDeleted).SingleOrDefaultAsync();
            likedOffer.TotalRating++;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DisLikeOfferAsync(int offerId, string userId)
        {
            var isExist = await _db.Reactions.Where(r => r.AuthorId == userId && r.OfferId == offerId)
                .SingleOrDefaultAsync();

            if (isExist != null)
            {
                throw new ArgumentException(string.Format(Constants.AlreadyTakeActionToThisOffer, offerId));
            }

            var reation = new Reaction
            {
                Action = false,
                AuthorId = userId,
                OfferId = offerId
            };

            _db.Reactions.Add(reation);

            var likedOffer = await _db.Offers.Where(o => o.Id == offerId && !o.IsDeleted).SingleOrDefaultAsync();
            likedOffer.TotalRating--;

            await _db.SaveChangesAsync();
            return true;
        }

        public IEnumerable<int> GetLikedOrDislikedOffersIds(string currentUserId)
        {
            return _db.Reactions
                .Where(r => r.AuthorId == currentUserId)
                .Select(x => x.OfferId)
                .ToList();
        }

        public async Task DeleteOfferAsync(int id)
        {
            var offer = await _db.Offers.Where(o => o.Id == id && !o.IsDeleted).SingleOrDefaultAsync();

            if (offer == null)
            {
                throw new ArgumentException(string.Format(Constants.OfferDoesNotExist, id));
            }

            offer.IsDeleted = true;
            var reviews = await _db.Reviews.Where(r => r.OfferId == offer.Id).ToListAsync();
            reviews.ForEach(x => { x.IsDeleted = true; });
            await _db.SaveChangesAsync();
        }
    }
}