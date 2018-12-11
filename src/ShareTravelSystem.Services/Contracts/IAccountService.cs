namespace ShareTravelSystem.Services.Contracts
{
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.ViewModels;
    using System.Threading.Tasks;

    public interface IAccountService
    {
        IActionResult Register(RegisterViewModel model);

        IActionResult Login(LoginViewModel model);

        Task<IActionResult> Logout();
    }
}
