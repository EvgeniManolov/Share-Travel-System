﻿namespace ShareTravelSystem.Data
{
    using Newtonsoft.Json;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DbInitializer
    {
        public static void Initialize(ShareTravelSystemDbContext context)
        {

            if (context.Towns.Any())
            {
                return;
            }

            string jsonTownsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\towns.json");

            string jsonTowns = File.ReadAllText(jsonTownsPath);
            List<Town> townsList = JsonConvert.DeserializeObject<List<Town>>(jsonTowns);

            context.Towns.AddRange(townsList);
            context.SaveChanges();
        }
    }
}
