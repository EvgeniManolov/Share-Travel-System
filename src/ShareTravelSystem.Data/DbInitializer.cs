using Newtonsoft.Json;
using ShareTravelSystem.Data.Models;
using ShareTravelSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShareTravelSystem.Data
{
    public class DbInitializer
    {
        public static void Initialize(ShareTravelSystemDbContext context)
        {
            // context.Database.EnsureCreated();
            
            if (context.Towns.Any())
            {
                return;
            }

            string townsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\towns.json");

            string json = File.ReadAllText(townsPath);
            var townsList = JsonConvert.DeserializeObject<List<Town>>(json);
            // да си оправя json да е само с име. и да оставя само градовете.
            foreach (var town in townsList)
            {
                var currentTown = new Town { Name = town.Name };
                context.Towns.Add(currentTown);
            }
            context.Towns.AddRange(townsList);
            context.SaveChanges();
        }
    }
}
