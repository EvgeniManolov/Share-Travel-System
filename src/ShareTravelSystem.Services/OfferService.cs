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


            Town departureTown = new Town { Name = model.DepartureTown };
            Town destinationTown = new Town { Name = model.DestinationTown };
            this.db.Towns.AddRange(departureTown, destinationTown);

            this.db.Offers.Add(new Offer
            {
                AuthorId = userId.ToString(),
                CreateDate = DateTime.UtcNow,
                DepartureDate = model.DepartureDate,
                Description = model.Description,
                Seat = model.Seat,
                Type = type,
                DepartureTownId = departureTown.Id,
                DestinationTownId = destinationTown.Id,
                TotalRating = 0,
                Price = model.Price
            });


            this.db.SaveChanges();
        }

        public List<DisplayOfferViewModel> GetAllOffers()
        {
            return this.db.Offers.Include(t => t.DepartureTown).Include(t => t.DestinationTown).OrderByDescending(t => t.CreateDate).Select(x => new DisplayOfferViewModel
            {
                Id = x.Id,
                Type = x.Type.ToString(),
                Author = x.Author.ToString(),
                DepartureDate = x.DepartureDate,
                Description = x.Description,
                Seat = x.Seat,
                DepartureTown = this.db.Towns.Where(t => t.Id == x.DepartureTownId).Select(a => a.Name).FirstOrDefault(),
                DestinationTown = x.DestinationTown.Name,
                Price = x.Price
            }).ToList();
    }

    public List<Town> GetAllTowns()
    {
        return this.db.Towns.ToList();
    }
}
}
