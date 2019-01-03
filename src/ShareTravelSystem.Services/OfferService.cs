namespace ShareTravelSystem.Services
{
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels.Offer;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using ShareTravelSystem.ViewModels.Review;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.Services.Infrastructure;

    public class OfferService : IOfferService
    {
        private readonly ShareTravelSystemDbContext db;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public OfferService(ShareTravelSystemDbContext db, UserManager<ShareTravelSystemUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public void CreateOffer(CreateOfferViewModel model, string userId)
        {
            if (!Enum.TryParse(model.Type, true, out OfferType type))
            {
                throw new ArgumentException(string.Format(Constants.OfferTypeDoesNotExist, model.Type));
            }

            int departureTownId = this.db
                                      .Towns
                                      .Where(t => t.Id == model.DepartureTownId && !t.IsDeleted)
                                      .Select(x => x.Id)
                                      .FirstOrDefault();

            int destinationTownId = this.db
                                        .Towns
                                        .Where(t => t.Id == model.DestinationTownId && !t.IsDeleted)
                                        .Select(x => x.Id)
                                        .FirstOrDefault();

            this.db.Offers.Add(new Offer
            {
                AuthorId = userId.ToString(),
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

            this.db.SaveChanges();
        }

        public DetailsOfferViewModel DetailsOffer(int Id)
        {
            DetailsOfferViewModel offer = this.db
                                              .Offers
                                              .Where(t => t.Id == Id && !t.IsDeleted)
                                              .ProjectTo<DetailsOfferViewModel>()
                                              .SingleOrDefault();
            if (offer == null)
            {
                throw new ArgumentException(string.Format(Constants.OfferDoesNotExist, Id));
            }

            offer.Reviews = this.db
                                .Reviews
                                .Where(r => r.OfferId == Id && !r.IsDeleted)
                                .OrderByDescending(x => x.CreateDate)
                                .Select(r => new DisplayReviewViewModel
                                {
                                    Id = r.Id,
                                    Content = r.Comment,
                                    Author = r.Author.UserName,
                                    CreateDate = r.CreateDate
                                }).ToList();

            return offer;
        }

        public OfferPaginationViewModel GetAllOffers(bool privateOffers, string filter, string search, string currentUserId, int page, int size)
        {
            List<DisplayOfferViewModel> offers = new List<DisplayOfferViewModel>();
            string titleOfPage = "";
            if (page == 0) page = 1;

            if (!privateOffers)
            {
                offers = this.db.Offers
                    .Where(o => !o.IsDeleted)
                    .OrderByDescending(x => x.CreateDate)
                    .ProjectTo<DisplayOfferViewModel>()
                    .ToList();

                titleOfPage = Constants.AllOffersTitlePageName;
            }
            else
            {
                offers = this.db.Offers
                     .Where(o => o.AuthorId == currentUserId && !o.IsDeleted)
                     .OrderByDescending(x => x.CreateDate)
                     .ProjectTo<DisplayOfferViewModel>()
                     .ToList();

                titleOfPage = Constants.MyOffersTitlePageName;
            }

            if (filter != null && filter != "" && filter != Constants.FilterOfAllOffers)
            {
                offers = offers
                         .Where(x => x.Type.ToLower() == filter.ToLower())
                         .ToList();
            }


            if (search != null && search != "")
            {
                offers = offers.Where(x => x.DepartureTownName.ToLower().Contains(search.Trim().ToLower()) || x.DestinationTownName.ToLower().Contains(search.Trim().ToLower())
                    || x.Description.ToLower().Contains(search.Trim().ToLower()))
                                .ToList();
            }

            int count = offers.Count();
            offers = offers.Skip((page - 1) * size).Take(size).ToList();

            OfferPaginationViewModel result = new OfferPaginationViewModel
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
            return this.db.Towns.Where(t => !t.IsDeleted).ToList();
        }

        public DisplayEditOfferViewModel GetOfferToEdit(int id, string currentUserId)
        {
            EditOfferViewModel model = this.db
                                           .Offers
                                           .Where(o => o.Id == id && !o.IsDeleted)
                                           .ProjectTo<EditOfferViewModel>()
                                           .SingleOrDefault();
            if (model == null)
            {
                throw new ArgumentException(string.Format(Constants.OfferDoesNotExist, model.Id));
            }

            string offerAuthor = this.db.Offers.Where(o => o.Id == id && !o.IsDeleted).Select(x => x.AuthorId).SingleOrDefault();
            if (offerAuthor != currentUserId)
            {
                throw new ArgumentException(string.Format(Constants.NotAuthorizedForThisOperation, currentUserId));
            }

            DisplayEditOfferViewModel result = new DisplayEditOfferViewModel
            {
                OfferModel = model,
                Towns = this.db.Towns.Where(t => !t.IsDeleted).ToList()
            };

            return result;
        }

        public void EditOffer(DisplayEditOfferViewModel model)
        {
            Offer offer = this.db.Offers.Where(o => o.Id == model.OfferModel.Id && !o.IsDeleted).SingleOrDefault();

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

            this.db.SaveChanges();
        }

        public bool LikeOffer(int offerId, string userId)
        {
            Reaction isExist = this.db.Reactions.Where(r => r.AuthorId == userId && r.OfferId == offerId).SingleOrDefault();

            if (isExist != null)
            {
                throw new ArgumentException(string.Format(Constants.AlreadyTakeActionToThisOffer, offerId));
            }
            Reaction reation = new Reaction
            {
                Action = true,
                AuthorId = userId,
                OfferId = offerId
            };

            this.db.Reactions.Add(reation);

            Offer likedOffer = this.db.Offers.Where(o => o.Id == offerId && !o.IsDeleted).SingleOrDefault();
            likedOffer.TotalRating++;

            this.db.SaveChanges();
            return true;
        }

        public bool DisLikeOffer(int offerId, string userId)
        {
            Reaction isExist = this.db.Reactions.Where(r => r.AuthorId == userId && r.OfferId == offerId).SingleOrDefault();

            if (isExist != null)
            {
                throw new ArgumentException(string.Format(Constants.AlreadyTakeActionToThisOffer, offerId));
            }
            Reaction reation = new Reaction
            {
                Action = false,
                AuthorId = userId,
                OfferId = offerId
            };

            this.db.Reactions.Add(reation);

            Offer likedOffer = this.db.Offers.Where(o => o.Id == offerId && !o.IsDeleted).SingleOrDefault();
            likedOffer.TotalRating--;

            this.db.SaveChanges();
            return true;
        }

        public ICollection<int> GetLikedOrDislikedOffersIds(string currentUserId)
        {
            return this.db.Reactions
                .Where(r => r.AuthorId == currentUserId)
                .Select(x => x.OfferId)
                .ToList();
        }

        public void DeleteOffer(int id)
        {
            Offer offer = this.db.Offers.Where(o => o.Id == id && !o.IsDeleted).SingleOrDefault();

            if (offer == null)
            {
                throw new ArgumentException(string.Format(Constants.OfferDoesNotExist, id));
            }
            offer.IsDeleted = true;
            List<Review> reviews = this.db.Reviews.Where(r => r.OfferId == offer.Id).ToList();
            reviews.ForEach(x => { x.IsDeleted = true; });
            this.db.SaveChanges();
        }
    }
}
