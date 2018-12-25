namespace ShareTravelSystem.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels.Offer;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ShareTravelSystem.Common;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ShareTravelSystem.ViewModels.Review;
    using ShareTravelSystem.ViewModels;

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
                throw new Exception("Invalid offer type");
                //  return this.BadRequestErrorWithView("Invalid offer type."); -- да имплементирам errors
            }


            int departureTownId = this.db.Towns.Where(t => t.Id == model.DepartureTownId).Select(x => x.Id).FirstOrDefault();
            int destinationTownId = this.db.Towns.Where(t => t.Id == model.DestinationTownId).Select(x => x.Id).FirstOrDefault();

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

        public DetailsOfferViewModel GetOfferById(int offerId)
        {
            DetailsOfferViewModel offer = this.db.Offers.Where(t => t.Id == offerId).ProjectTo<DetailsOfferViewModel>().SingleOrDefault();
            offer.Reviews = this.db.Reviews.Where(r => r.OfferId == offerId).OrderByDescending(x => x.CreateDate).Select(r => new DisplayReviewViewModel
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
            if (!privateOffers)
            {
                offers = this.db.Offers
                         .OrderByDescending(x => x.CreateDate)
                         .ProjectTo<DisplayOfferViewModel>()
                         .ToList();
                titleOfPage = "All Offers";
            }
            else
            {
                offers = this.db.Offers
                         .Where(o => o.AuthorId == currentUserId)
                         .OrderByDescending(x => x.CreateDate)
                         .ProjectTo<DisplayOfferViewModel>()
                         .ToList();
                titleOfPage = "My Offers";
            }

            if (filter != null && filter != "" && filter != "All")
            {
                offers = offers
                         .Where(x => x.Type.ToLower() == filter.ToLower())
                         .ToList();
            }


            if (search != null && search != "")
            {
                offers = offers.Where(x => x.DepartureTownName.ToLower().Contains(search.Trim().ToLower())
                                || x.DestinationTownName.ToLower().Contains(search.Trim().ToLower()))
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
            return this.db.Towns.ToList();
        }



        public DisplayEditOfferViewModel GetOfferToEdit(int id)
        {
            EditOfferViewModel model = this.db.Offers.Where(o => o.Id == id).ProjectTo<EditOfferViewModel>().SingleOrDefault();
            DisplayEditOfferViewModel result = new DisplayEditOfferViewModel
            {
                OfferModel = model,
                Towns = this.db.Towns.ToList()
            };
            return result;
        }

    }
}
