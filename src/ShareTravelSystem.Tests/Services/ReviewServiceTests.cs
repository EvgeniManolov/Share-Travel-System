namespace ShareTravelSystem.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ShareTravelSystem.Services;
    using ViewModels.Review;
    using Web.Areas.Identity.Data;
    using Web.Models;
    using Xunit;

    public class ReviewServiceTests
    {
        private UserManager<ShareTravelSystemUser> userManager { get; set; }

        public ReviewServiceTests()
        {
            userManager = TestStartup.UserManager;
        }

        [Fact]
        public async Task CreateReviewAsync_WithCorrectData_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                ReviewService reviewService = new ReviewService(context, userManager);
                OfferService offerService = new OfferService(context, userManager);

                List<ShareTravelSystemUser> users = new List<ShareTravelSystemUser>
                {
                    new ShareTravelSystemUser{ UserName = "TestUser1" },
                    new ShareTravelSystemUser{ UserName = "TestUser2"}
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();

                ShareTravelSystemUser testUser1 = await context.Users.Where(x => x.UserName == "TestUser1").SingleOrDefaultAsync();
                ShareTravelSystemUser testUser2 = await context.Users.Where(x => x.UserName == "TestUser2").SingleOrDefaultAsync();

                Offer offer = new Offer
                {
                    Type = OfferType.Search,
                    DepartureTownId = 1,
                    DestinationTownId = 2,
                    Seat = 2,
                    Price = 10,
                    DepartureDate = DateTime.UtcNow,
                    Description = "Здравейте!",
                    Author = testUser1,
                    TotalRating = 0,
                    CreateDate = DateTime.UtcNow
                };

                await context.Offers.AddAsync(offer);
                await context.SaveChangesAsync();

                // Act
                await reviewService.CreateReviewAsync("TestComment", offer.Id, testUser2.Id);

                // Assert
                Assert.True(await context.Reviews.Where(o => o.OfferId == offer.Id).CountAsync() == 1);
                ShareTravelSystemUser reviewAuthor = await context.Reviews.Where(x => x.OfferId == offer.Id).Select(t => t.Author).SingleOrDefaultAsync();
                Assert.Equal(reviewAuthor, testUser2);
            }
        }

        [Fact]
        public async Task GetReviewToEditAsync_WithCorrectId_ReturnsCorrectReview()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                ReviewService reviewService = new ReviewService(context, userManager);
                OfferService offerService = new OfferService(context, userManager);

                List<ShareTravelSystemUser> users = new List<ShareTravelSystemUser>
                {
                    new ShareTravelSystemUser{ UserName = "TestUser1" },
                    new ShareTravelSystemUser{ UserName = "TestUser2"}
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();

                ShareTravelSystemUser testUser1 = await context.Users.Where(x => x.UserName == "TestUser1").SingleOrDefaultAsync();
                ShareTravelSystemUser testUser2 = await context.Users.Where(x => x.UserName == "TestUser2").SingleOrDefaultAsync();

                Offer offer = new Offer
                {
                    Type = OfferType.Search,
                    DepartureTownId = 1,
                    DestinationTownId = 2,
                    Seat = 2,
                    Price = 10,
                    DepartureDate = DateTime.UtcNow,
                    Description = "Здравейте!",
                    Author = testUser1,
                    TotalRating = 0,
                    CreateDate = DateTime.UtcNow
                };

                await context.Offers.AddAsync(offer);
                await context.SaveChangesAsync();

                Review review = new Review
                {
                    Comment = "TestComment",
                    Author = testUser2,
                    OfferId = offer.Id,
                    CreateDate = DateTime.UtcNow
                };

                await context.Reviews.AddAsync(review);
                await context.SaveChangesAsync();

                // Act
                EditReviewViewModel model = await reviewService.GetReviewToEditAsync(review.Id, offer.Id, testUser2.Id);

                // Assert
                Assert.False(review.IsDeleted);
                Assert.NotNull(model);
                Assert.Equal(review.Comment, model.Comment);
            }
        }

        [Fact]
        public async Task EditReviewAsync_WithCorrectModel_WorksCorrectly()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                ReviewService reviewService = new ReviewService(context, userManager);
                OfferService offerService = new OfferService(context, userManager);

                List<ShareTravelSystemUser> users = new List<ShareTravelSystemUser>
                {
                    new ShareTravelSystemUser{ UserName = "TestUser1" },
                    new ShareTravelSystemUser{ UserName = "TestUser2"}
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();

                ShareTravelSystemUser testUser1 = await context.Users.Where(x => x.UserName == "TestUser1").SingleOrDefaultAsync();
                ShareTravelSystemUser testUser2 = await context.Users.Where(x => x.UserName == "TestUser2").SingleOrDefaultAsync();

                Offer offer = new Offer
                {
                    Type = OfferType.Search,
                    DepartureTownId = 1,
                    DestinationTownId = 2,
                    Seat = 2,
                    Price = 10,
                    DepartureDate = DateTime.UtcNow,
                    Description = "Здравейте!",
                    Author = testUser1,
                    TotalRating = 0,
                    CreateDate = DateTime.UtcNow
                };

                await context.Offers.AddAsync(offer);
                await context.SaveChangesAsync();

                Review review = new Review
                {
                    Comment = "TestComment",
                    Author = testUser2,
                    OfferId = offer.Id,
                    CreateDate = DateTime.UtcNow
                };

                await context.Reviews.AddAsync(review);
                await context.SaveChangesAsync();

                EditReviewViewModel model = new EditReviewViewModel
                {
                    Id = review.Id,
                    OfferId = offer.Id,
                    Comment = "TestCommentChange"
                };

                // Act
                await reviewService.EditReviewAsync(model);

                Review reviewCommentDb = await context.Reviews.Where(x => x.OfferId == offer.Id && x.Author == testUser2).SingleOrDefaultAsync();

                // Assert
                Assert.Equal("TestCommentChange", reviewCommentDb.Comment);
            }
        }

        [Fact]
        public async Task DeleteReviewAsync_WithCorrectId_SetFlagDeleteToTrue()
        {
            using (var context = new ShareTravelSystemDbContext(CreateNewContextOptions()))
            {
                // Arrange
                ReviewService reviewService = new ReviewService(context, userManager);
                OfferService offerService = new OfferService(context, userManager);

                List<ShareTravelSystemUser> users = new List<ShareTravelSystemUser>
                {
                    new ShareTravelSystemUser{ UserName = "TestUser1" },
                    new ShareTravelSystemUser{ UserName = "TestUser2"}
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();

                ShareTravelSystemUser testUser1 = await context.Users.Where(x => x.UserName == "TestUser1").SingleOrDefaultAsync();
                ShareTravelSystemUser testUser2 = await context.Users.Where(x => x.UserName == "TestUser2").SingleOrDefaultAsync();

                Offer offer = new Offer
                {
                    Type = OfferType.Search,
                    DepartureTownId = 1,
                    DestinationTownId = 2,
                    Seat = 2,
                    Price = 10,
                    DepartureDate = DateTime.UtcNow,
                    Description = "Здравейте!",
                    Author = testUser1,
                    TotalRating = 0,
                    CreateDate = DateTime.UtcNow
                };

                await context.Offers.AddAsync(offer);
                await context.SaveChangesAsync();

                Review review = new Review
                {
                    Comment = "TestComment",
                    Author = testUser2,
                    OfferId = offer.Id,
                    CreateDate = DateTime.UtcNow
                };

                await context.Reviews.AddAsync(review);
                await context.SaveChangesAsync();


                // Act
                await reviewService.DeleteReviewAsync(review.Id, offer.Id);

                Review deletedReviewDb = await context.Reviews.Where(x => x.OfferId == offer.Id && x.Author == testUser2).SingleOrDefaultAsync();

                // Assert
                Assert.NotNull(deletedReviewDb);
                Assert.True(deletedReviewDb.IsDeleted);
            }
        }

        private static DbContextOptions<ShareTravelSystemDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ShareTravelSystemDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
