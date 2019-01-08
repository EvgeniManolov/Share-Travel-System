namespace ShareTravelSystem.Services.Contracts
{
    using ShareTravelSystem.ViewModels;
    using System.Threading.Tasks;

    public interface IAccountService
    {
        Task<bool> Register(RegisterViewModel model);

        Task<bool> Login(LoginViewModel model);

        Task<bool> Logout();
    }
}
