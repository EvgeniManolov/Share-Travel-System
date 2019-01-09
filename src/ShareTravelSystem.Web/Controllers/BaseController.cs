namespace ShareTravelSystem.Web.Controllers
{
    using Areas.Identity.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        private readonly UserManager<ShareTravelSystemUser> userManager;

        public BaseController(UserManager<ShareTravelSystemUser> userManager)
        {
            this.userManager = userManager;
        }

        public BaseController()
        {
        }


        protected bool RedirectIfNotInRole(string role)
        {
            return User == null || this.User.IsInRole(role);
        }

        protected RedirectToActionResult MakeRedirectResult(string area, string controller, string action, int id)
        {
            var controllerName = controller.Replace("Controller", "");
            var result = new RedirectToActionResult(action, controllerName, new {id, Area = area});

            return result;
        }
    }
}