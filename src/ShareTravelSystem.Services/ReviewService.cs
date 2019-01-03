namespace ShareTravelSystem.Services
{
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Services.Infrastructure;
    using ShareTravelSystem.ViewModels.Review;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Linq;

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

        public void CreateReview(string comment, int offerId, string currentUserId)
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

            this.db.Reviews.Add(review);
            this.db.SaveChanges();
        }

        public EditReviewViewModel GetReviewToEdit(int id, int offerId, string currentUserId)
        {
            EditReviewViewModel review = this.db.Reviews.Where(r => r.Id == id && !r.IsDeleted).Select(e => new EditReviewViewModel
            {
                Id = id,
                Comment = e.Comment,
                OfferId = offerId
            }).SingleOrDefault();

            if (review == null)
            {
                throw new ArgumentException(string.Format(Constants.ReviewDoesNotExist, id));
            }

            string reviewAuthor = this.db.Reviews.Where(r => r.Id == id && !r.IsDeleted).Select(x => x.AuthorId).SingleOrDefault();
            if (reviewAuthor != currentUserId)
            {
                throw new ArgumentException(string.Format(Constants.NotAuthorizedForThisOperation, currentUserId));
            }
            return review;
        }


        public void EditReview(EditReviewViewModel model)
        {
            Review review = this.db.Reviews.Where(r => r.Id == model.Id && r.OfferId == model.OfferId && !r.IsDeleted).SingleOrDefault();

            review.Comment = model.Comment;
            this.db.SaveChanges();
        }

        public void DeleteReview(int reviewId, int offerId)
        {
            Review review = this.db.Reviews.Where(r => r.Id == reviewId && r.OfferId == offerId && !r.IsDeleted).SingleOrDefault();

            if (review == null)
            {
                throw new ArgumentException(string.Format(Constants.ReviewDoesNotExist, reviewId));
            }

            review.IsDeleted = true;
            this.db.SaveChanges();
        }
    }
}
