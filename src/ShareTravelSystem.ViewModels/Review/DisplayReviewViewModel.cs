using System;
using System.Collections.Generic;
using System.Text;

namespace ShareTravelSystem.ViewModels.Review
{
    public class DisplayReviewViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
