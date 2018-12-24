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

        public IEnumerable<DisplayOfferViewModel> GetAllOffers(string filter, string currentUserId)
        {
            List<DisplayOfferViewModel> result = new List<DisplayOfferViewModel>();
            if (filter.ToLower() == "all")
            {
                result = this.db.Offers.OrderByDescending(x => x.CreateDate).ProjectTo<DisplayOfferViewModel>().ToList();
            }
            if (filter.ToLower() == "my")
            {
                result = this.db.Offers.Where(a => a.AuthorId == currentUserId).OrderByDescending(x => x.CreateDate).ProjectTo<DisplayOfferViewModel>().ToList();
            }
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
