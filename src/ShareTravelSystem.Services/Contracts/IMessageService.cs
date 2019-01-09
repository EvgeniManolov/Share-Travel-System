namespace ShareTravelSystem.Services.Contracts
{
    using ViewModels;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task CreateMessageAsync(string message, string userId);

        Task<MessagePaginationViewModel> GetAllMessagesAsync(string search, int page);
    }
}