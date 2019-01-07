namespace ShareTravelSystem.Services
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Services.Infrastructure;
    using ShareTravelSystem.ViewModels.Review;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReviewService : IReviewService
    {
        private readonly ShareTravelSystemDbContext db;
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public ReviewService(ShareTravelSystemDbContext db,
                             UserManager<ShareTravelSystemUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task CreateReviewAsync(string comment, int offerId, string currentUserId)
        {
            if (offerId == 0)
            {
                throw new ArgumentException(string.Format(Constants.OfferDoesNotExist, offerId));
            }

            Review review = new Review
            {
                Comment = comment,
                OfferId = offerId,
                AuthorId = currentUserId,
                CreateDate = DateTime.UtcNow
            };

            await this.db.Reviews.AddAsync(review);
            await this.db.SaveChangesAsync();
        }

        public async Task<EditReviewViewModel> GetReviewToEditAsync(int id, int offerId, string currentUserId)
        {
            EditReviewViewModel review = await this.db.Reviews.Where(r => r.Id == id && !r.IsDeleted).ProjectTo<EditReviewViewModel>().SingleOrDefaultAsync();

            if (review == null)
            {
                throw new ArgumentException(string.Format(Constants.ReviewDoesNotExist, id));
            }

            string reviewAuthor = await this.db.Reviews.Where(r => r.Id == id && !r.IsDeleted).Select(x => x.AuthorId).SingleOrDefaultAsync();
            if (reviewAuthor != currentUserId)
            {
                throw new ArgumentException(string.Format(Constants.NotAuthorizedForThisOperation, currentUserId));
            }
            return review;
        }


        public async Task EditReviewAsync(EditReviewViewModel model)
        {
            Review review = await this.db.Reviews.Where(r => r.Id == model.Id && r.OfferId == model.OfferId && !r.IsDeleted).SingleOrDefaultAsync();

            review.Comment = model.Comment;
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int reviewId, int offerId)
        {
            Review review = await this.db.Reviews.Where(r => r.Id == reviewId && r.OfferId == offerId && !r.IsDeleted).SingleOrDefaultAsync();

            if (review == null)
            {
                throw new ArgumentException(string.Format(Constants.ReviewDoesNotExist, reviewId));
            }

            review.IsDeleted = true;
            await this.db.SaveChangesAsync();
        }
    }
}
