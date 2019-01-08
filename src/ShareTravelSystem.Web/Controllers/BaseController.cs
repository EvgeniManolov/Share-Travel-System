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
            if (this.User != null)
            {
                return this.User.IsInRole(role);
            }
            return true;
        }

        protected RedirectToActionResult MakeRedirectResult(string area, string controller, string action, int id)
        {
            string controllerName = controller.Replace("Controller", "");
            RedirectToActionResult result = new RedirectToActionResult(action, controllerName, new {id, Area = area });

            return result;
        }
    }
}
