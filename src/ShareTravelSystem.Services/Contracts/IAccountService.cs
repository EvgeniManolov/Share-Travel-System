namespace ShareTravelSystem.Services.Contracts
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public interface IAccountService
    {
        IActionResult Register(RegisterViewModel model);

        IActionResult Login(LoginViewModel model);

        Task<IActionResult> Logout();
    }
}
