namespace ShareTravelSystem.ViewModels.Messages
{
    using ShareTravelSystem.Common;
    using System;

    public class DisplayMessageViewModel : IMapFrom<Data.Models.Message>
    {
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
