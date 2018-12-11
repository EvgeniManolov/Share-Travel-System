namespace ShareTravelSystem.Services
{
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.ViewModels;
    using System;
    using System.Threading.Tasks;

    public class AccountService : IAccountService
    {
        public IActionResult Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        public IActionResult Register(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
