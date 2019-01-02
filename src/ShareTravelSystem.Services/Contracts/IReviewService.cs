namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels.Review;

    public interface IReviewService
    {
        void CreateReview(string comment, int offerId, string userId);

        EditReviewViewModel GetToEditReviewById(int id,int offerId, string currentUserId);

        void EditReviewById(EditReviewViewModel model);

        void DeleteReviewById(int reviewId, int offerId);
    }
}
 