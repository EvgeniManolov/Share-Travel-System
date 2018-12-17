namespace ShareTravelSystem.Services
{
    using Microsoft.AspNetCore.Identity;
    using Newtonsoft.Json;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels.Offer;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;

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

            string townsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\towns.json");

            string json = File.ReadAllText(townsPath);
            var playerList = JsonConvert.DeserializeObject<List<Town>>(json);

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
    }
}
