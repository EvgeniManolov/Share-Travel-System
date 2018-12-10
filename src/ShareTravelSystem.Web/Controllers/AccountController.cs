namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Data.Models;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using System.Threading.Tasks;

    public class AccountController : Controller
    {
        private readonly UserManager<ShareTravelSystemUser> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<ShareTravelSystemUser> signInManager;

        public AccountController(
            UserManager<ShareTravelSystemUser> userManager,
            RoleManager<Role> roleManager,
            SignInManager<ShareTravelSystemUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
    }
}
