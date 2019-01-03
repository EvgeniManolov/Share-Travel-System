namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels.Review;

    public interface IReviewService
    {
        void CreateReview(string comment, int offerId, string userId);

        EditReviewViewModel GetReviewToEdit(int id,int offerId, string currentUserId);

        void EditReview(EditReviewViewModel model);

        void DeleteReview(int reviewId, int offerId);
    }
}
 