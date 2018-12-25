using System;
using System.Collections.Generic;
using System.Text;

namespace ShareTravelSystem.ViewModels
{
    public abstract class PaginationViewModel
    {   
        public int Size { get; set; }

        public int Page { get; set; }

        public int Count { get; set; }
    }
}
