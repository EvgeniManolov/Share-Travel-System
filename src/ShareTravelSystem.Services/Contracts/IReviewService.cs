using System;
using System.Collections.Generic;
using System.Text;

namespace ShareTravelSystem.Services.Contracts
{
    public interface IReviewService
    {
        void CreateReview(string comment, int offerId, string currentUserId);
    }
}
