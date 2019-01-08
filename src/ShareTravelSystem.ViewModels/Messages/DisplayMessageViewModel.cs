namespace ShareTravelSystem.ViewModels.Messages
{
    using System;
    using Common;
    using Data.Models;

    public class DisplayMessageViewModel : IMapFrom<Message>
    {
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
