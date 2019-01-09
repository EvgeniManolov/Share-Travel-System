namespace ShareTravelSystem.ViewModels.Pagination
{
    using Abstract;
    using Messages;

    public class MessagePaginationViewModel : PaginationViewModel
    {
        public string Search { get; set; }

        public DisplayAllMessagesViewModel Messages { get; set; }
    }
}