using System;
using System.Collections.Generic;
using System.Text;

namespace ShareTravelSystem.ViewModels.Messages
{
    public class DisplayAllMessagesViewModel
    {
        public IEnumerable<DisplayMessageViewModel> Messages { get; set; }
    }
}
