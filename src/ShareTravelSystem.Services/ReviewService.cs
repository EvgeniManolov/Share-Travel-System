namespace ShareTravelSystem.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ViewModels.Review;
    using Web.Areas.Identity.Data;
    using Web.Models;

    public class ReviewService : IReviewService
    {
        private readonly ShareTravelSystemDbContext _db;
        private readonly UserManager<ShareTravelSystemUser> _userManager;

        public ReviewService(ShareTravelSystemDbContext db,
            UserManager<ShareTravelSystemUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        public async Task CreateReviewAsync(string comment, int offerId, string currentUserId)
        {
            var offer = await _db.Offers.Where(x => x.Id == offerId).SingleOrDefaultAsync();
            if (offer == null)
            {
                throw new ArgumentException(string.Format(Constants.OfferDoesNotExist, offerId));
            }

            var review = new Review
            {
                Comment = comment,
                OfferId = offerId,
                AuthorId = currentUserId,
                CreateDate = DateTime.UtcNow
            };

            await _db.Reviews.AddAsync(review);
            await _db.SaveChangesAsync();
        }

        public async Task<EditReviewViewModel> GetReviewToEditAsync(int id, int offerId, string currentUserId)
        {
            var review = await _db.Reviews.Where(r => r.Id == id && !r.IsDeleted)
                .ProjectTo<EditReviewViewModel>().SingleOrDefaultAsync();

            if (review == null)
            {
                throw new ArgumentException(string.Format(Constants.ReviewDoesNotExist, id));
            }

            var reviewAuthor = await _db.Reviews.Where(r => r.Id == id && !r.IsDeleted).Select(x => x.AuthorId)
                .SingleOrDefaultAsync();
            if (reviewAuthor != currentUserId)
            {
                throw new ArgumentException(string.Format(Constants.NotAuthorizedForThisOperation, currentUserId));
            }

            return review;
        }


        public async Task EditReviewAsync(EditReviewViewModel model)
        {
            var review = await _db.Reviews
                .Where(r => r.Id == model.Id && r.OfferId == model.OfferId && !r.IsDeleted).SingleOrDefaultAsync();

            review.Comment = model.Comment;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int reviewId, int offerId)
        {
            var review = await _db.Reviews.Where(r => r.Id == reviewId && r.OfferId == offerId && !r.IsDeleted)
                .SingleOrDefaultAsync();

            if (review == null)
            {
                throw new ArgumentException(string.Format(Constants.ReviewDoesNotExist, reviewId));
            }

            review.IsDeleted = true;
            await _db.SaveChangesAsync();
        }
    }
}