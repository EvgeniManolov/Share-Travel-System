namespace ShareTravelSystem.Tests
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Infrastructure.Mapping;

    public class TestStartup
    {
        private static bool isInitialized;

        private static Mock<UserManager<ShareTravelSystemUser>> userManager;

        static TestStartup()
        {
            if (!isInitialized)
            {
                Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfile()));

                isInitialized = true;
            }

            if (userManager == null)
            {
                var mockUserStore = new Mock<IUserStore<ShareTravelSystemUser>>();
                userManager = new Mock<UserManager<ShareTravelSystemUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            }
        }

        public static UserManager<ShareTravelSystemUser> UserManager { get { return userManager.Object; } }

    }
}
