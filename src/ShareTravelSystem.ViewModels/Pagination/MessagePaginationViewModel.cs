using ShareTravelSystem.ViewModels.Messages;

namespace ShareTravelSystem.ViewModels
{
    public class MessagePaginationViewModel : PaginationViewModel
    {
        public string Search { get; set; }

        public DisplayAllMessagesViewModel Messages { get; set; }
    }
}
