namespace ShareTravelSystem.Tests
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Web.Areas.Identity.Data;
    using Web.Infrastructure.Mapping;

    public class TestStartup
    {
        private static readonly bool isInitialized;

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
