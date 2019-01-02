namespace ShareTravelSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using System.Threading.Tasks;

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
            return this.User.IsInRole(role);
        }

        protected RedirectToActionResult MakeRedirectResult(string area, string controller, string action, int id)
        {
            string controllerName = controller.Replace("Controller", "");
            RedirectToActionResult result = new RedirectToActionResult(action, controllerName, new { id = id, Area = area });

            return result;
        }
    }
}
