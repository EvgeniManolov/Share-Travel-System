namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Services.Contracts;
    using ShareTravelSystem.Web.Infrastructure.Constants;
    using ShareTravelSystem.ViewModels;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using System.Linq;
    using System.Threading.Tasks;


    public class AccountController : BaseController
    {
        private readonly UserManager<ShareTravelSystemUser> userManager;
        private readonly SignInManager<ShareTravelSystemUser> signInManager;
        //private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAccountService accountsService;

        public AccountController(
            UserManager<ShareTravelSystemUser> userManager,
            SignInManager<ShareTravelSystemUser> signInManager,
            IAccountService accountsService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accountsService = accountsService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // this.logger.LogInformation("User logged in.");
                    return RedirectToLocal(model.Email);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var isExist = this.userManager.FindByEmailAsync(model.Email).GetAwaiter().GetResult();

            if (isExist != null)
            {
                this.ModelState.AddModelError("Name", Constants.UserAlreadyExists);
                return this.View(model);
            }

            if (ModelState.IsValid)
            {
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
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            return View(model);
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            // this.logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl, string userName = "")
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            ViewData["url"] = returnUrl;
            return View();
        }
    }
}
