namespace ShareTravelSystem.Services
{
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;

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
    }
}
