using System;
using System.Collections.Generic;
using System.Text;

namespace ShareTravelSystem.ViewModels.Announcement
{
    public class DetailsAnnouncementViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public string Author { get; set; }
    }
}
