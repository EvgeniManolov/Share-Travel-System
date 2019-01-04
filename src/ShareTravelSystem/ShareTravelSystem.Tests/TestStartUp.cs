namespace ShareTravelSystem.Tests
{
    using AutoMapper;
    using ShareTravelSystem.Web.Infrastructure.Mapping;

    public class TestStartUp
    {
        private static bool isInitialized;

        public static void Initialize()
        {
            if (!isInitialized)
            {
                Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfile()));

                isInitialized = true;
            }
        }

    }
}
