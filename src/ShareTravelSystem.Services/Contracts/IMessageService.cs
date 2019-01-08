namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels.Messages;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task CreateMessageAsync(string message, string userId);
        Task<DisplayAllMessagesViewModel> GetAllMessagesAsync();
    }
}
