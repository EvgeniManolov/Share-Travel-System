namespace ShareTravelSystem.Services.Contracts
{
    public interface IReviewService
    {
        void CreateReview(string comment, int offerId, string userId);
    }
}
