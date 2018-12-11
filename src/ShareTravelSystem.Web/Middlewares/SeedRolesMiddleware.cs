namespace ShareTravelSystem.Web.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using ShareTravelSystem.Web.Areas.Identity.Data;
    using ShareTravelSystem.Web.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;
       
        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
       
        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider, UserManager<ShareTravelSystemUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var dbContext = serviceProvider.GetService<ShareTravelSystemDbContext>();
       
            if (!dbContext.Roles.Any())
            {
                await this.SeedRoles(userManager, roleManager);
            }
       
            await this.next(context);
        }
       
        private async Task SeedRoles(UserManager<ShareTravelSystemUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));
        }
    }
}
