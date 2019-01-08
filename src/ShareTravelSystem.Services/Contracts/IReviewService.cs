namespace ShareTravelSystem.Services.Contracts
{
    using System.Threading.Tasks;
    using ViewModels.Review;

    public interface IReviewService
    {
        Task CreateReviewAsync(string comment, int offerId, string userId);

        Task<EditReviewViewModel> GetReviewToEditAsync(int id, int offerId, string currentUserId);

        Task EditReviewAsync(EditReviewViewModel model);

        Task DeleteReviewAsync(int reviewId, int offerId);
    }
}
