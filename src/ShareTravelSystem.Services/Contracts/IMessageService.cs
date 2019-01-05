namespace ShareTravelSystem.Services.Contracts
{
    using System.Threading.Tasks;
    public interface IMessageService
    {
        Task CreateMessageAsync(string message, string userId);
    }
}
