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

            return offer;
        }

        public ICollection<DisplayOfferViewModel> GetAllOffers()
        {
            ICollection<DisplayOfferViewModel> result = this.db.Offers.OrderByDescending(t => t.CreateDate).ProjectTo<DisplayOfferViewModel>().ToList();

            return result;
        }

        public List<Town> GetAllTowns()
        {
            return this.db.Towns.ToList();
        }
    }
}
