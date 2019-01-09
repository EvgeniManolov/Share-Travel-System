namespace ShareTravelSystem.Services.Infrastructure
{
    public class Constants
    {
        public const string TownAlreadyExists = "Town with name {0} already exists.";

        public const string TownDoesNotExist = "Town with id: {0} does not exist.";

        public const string AnnouncementDoesNotExist = "Announcement with id: {0} does not exist.";

        public const string OfferDoesNotExist = "Offer with id: {0} does not exist.";

        public const string OfferTypeDoesNotExist = "OfferType with name {0} does not exist.";

        public const string AlreadyTakeActionToThisOffer = "Already take actions to offer with id: {0}.";

        public const string ReviewDoesNotExist = "Review with id: {0} does not exist.";

        public const string NotAuthorizedForThisOperation = "User with id: {0} is not authorized for this operation.";

        public const string MyOffersTitlePageName = "My Offers";

        public const string AllOffersTitlePageName = "All Offers";

        public const string FilterOfAllOffers = "All";

        public const int TownsPerPage = 10;

        public const int AnnouncementsPerPage = 8;

        public const int UserStatisticsPerPage = 10;

        public const int OffersPerPage = 8;

        public const string AdminRole = "Admin";

        public const string UserRole = "User";

        public const string UserAlreadyExists = "User with this Username/Email already exists!";
    }
}