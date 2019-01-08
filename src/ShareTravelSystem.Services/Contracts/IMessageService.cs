namespace ShareTravelSystem.Services.Contracts
{
    using System.Threading.Tasks;
    using ViewModels.Messages;

    public interface IMessageService
    {
        Task CreateMessageAsync(string message, string userId);
        Task<DisplayAllMessagesViewModel> GetAllMessagesAsync();
    }
}
