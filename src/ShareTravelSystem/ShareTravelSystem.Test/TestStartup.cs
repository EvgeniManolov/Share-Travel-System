using ShareTravelSystem.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareTravelSystem.Tests
{
    public class TestStartup
    {
        public static object thisLock = new object();
        // Centralize automapper initialize
        public static void Initialize()
        {
            // This will ensure one thread can access to this static initialize call
            // and ensure the mapper is reseted before initialized
            lock (thisLock)
            {
                AutoMapper.Mapper.Reset();
                AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfile()));
            }
        }
    }
}
