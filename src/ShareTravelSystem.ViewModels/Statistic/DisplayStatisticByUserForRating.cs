using System;
using System.Collections.Generic;
using System.Text;

namespace ShareTravelSystem.ViewModels.Statistic
{
    public class DisplayStatisticByUserForRating
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public int TotalLikes { get; set; }

        public int TotalDisLikes { get; set; }

        public int TotalRating { get; set; } 
    }
}
