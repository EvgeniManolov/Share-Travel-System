namespace ShareTravelSystem.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Services.Infrastructure;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccountService : IAccountService
    {
        private readonly SignInManager<ShareTravelSystemUser> signInManager;
        private readonly UserManager<ShareTravelSystemUser> userManager;
        private readonly ShareTravelSystemDbContext context;

        public AccountService(SignInManager<ShareTravelSystemUser> signInManager, UserManager<ShareTravelSystemUser> userManager, ShareTravelSystemDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
        }


        public async Task<bool> Register(RegisterViewModel model)
        {
            var isExist = this.userManager.FindByEmailAsync(model.Email).GetAwaiter().GetResult();

            if (isExist != null)
            {
                throw new ArgumentException("Name", Constants.UserAlreadyExists);
            }
            if (model.Password != model.ConfirmPassword)
            {
                return false;
            }


            var user = new ShareTravelSystemUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (this.userManager.Users.Count() == 1)
                {
                    await this.userManager.AddToRoleAsync(user, Constants.AdminRole);
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, Constants.UserRole);
                }

                this.signInManager.SignInAsync(user, false).Wait();

            }
            return true;
        }

        public bool Login(string username, string password, bool rememberMe)
        {
            var result = this.signInManager.PasswordSignInAsync(username, password, rememberMe, true).Result;

            return result.Succeeded;
        }


        public async Task<bool> Login(LoginViewModel model)
        {
            var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Logout()
        {
            await this.signInManager.SignOutAsync();
            return true;
        }
    }
}
