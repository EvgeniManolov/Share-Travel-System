﻿using ShareTravelSystem.ViewModels.Offer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareTravelSystem.ViewModels
{
    public class AnnouncementPaginationViewModel : PaginationViewModel
    {
        public string Search { get; set; }

        public bool PrivateAnnouncements { get; set; }

        public string TitleOfPage { get; set; }

        public DisplayAllAnnouncementsViewModel AllAnnouncements { get; set; }
    }
}