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

        public void Create(CreateOfferViewModel model, string userId)
        {
            if (!Enum.TryParse(model.Type, true, out OfferType type))
            {
                throw new ArgumentException(string.Format(Constants.OfferTypeDoesNotExist, model.Type));
            }

            int departureTownId = this.db
                                      .Towns
                                      .Where(t => t.Id == model.DepartureTownId && t.IsDeleted == false)
                                      .Select(x => x.Id)
                                      .FirstOrDefault();

            int destinationTownId = this.db
                                        .Towns
                                        .Where(t => t.Id == model.DestinationTownId && t.IsDeleted == false)
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

        public DetailsOfferViewModel GetOfferById(int Id)
        {
            DetailsOfferViewModel offer = this.db
                                              .Offers
                                              .Where(t => t.Id == Id)
                                              .ProjectTo<DetailsOfferViewModel>()
                                              .SingleOrDefault();
            if (offer == null)
            {
                throw new ArgumentException(string.Format(Constants.OfferDoesNotExist, Id));
            }

            offer.Reviews = this.db
                                .Reviews
                                .Where(r => r.OfferId == Id)
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
                                .OrderByDescending(x => x.CreateDate)
                                .ProjectTo<DisplayOfferViewModel>()
                                .ToList();

                titleOfPage = Constants.AllOffersTitlePageName;
            }
            else
            {
                offers = this.db.Offers
                                .Where(o => o.AuthorId == currentUserId)
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
            return this.db.Towns.Where(t => t.IsDeleted == false).ToList();
        }

        public DisplayEditOfferViewModel GetOfferToEdit(int id)
        {
            EditOfferViewModel model = this.db
                                           .Offers
                                           .Where(o => o.Id == id)
                                           .ProjectTo<EditOfferViewModel>()
                                           .SingleOrDefault();
            if (model == null)
            {
                throw new ArgumentException(string.Format(Constants.OfferDoesNotExist, model.Id));
            }

            DisplayEditOfferViewModel result = new DisplayEditOfferViewModel
            {
                OfferModel = model,
                Towns = this.db.Towns.Where(t => t.IsDeleted == false).ToList()
            };

            return result;
        }

        public void EditOffer(DisplayEditOfferViewModel model)
        {
            Offer offer = this.db.Offers.Where(o => o.Id == model.OfferModel.Id).SingleOrDefault();

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
    }
}
