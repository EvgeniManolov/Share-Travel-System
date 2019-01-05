namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels.Review;
    using System.Threading.Tasks;

    public interface IReviewService
    {
        Task CreateReviewAsync(string comment, int offerId, string userId);

        Task<EditReviewViewModel> GetReviewToEditAsync(int id, int offerId, string currentUserId);

        Task EditReviewAsync(EditReviewViewModel model);

        Task DeleteReviewAsync(int reviewId, int offerId);
    }
}
