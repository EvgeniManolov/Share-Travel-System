namespace ShareTravelSystem.Web.Middlewares
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Areas.Identity.Data;
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider,
            UserManager<ShareTravelSystemUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var dbContext = serviceProvider.GetService<ShareTravelSystemDbContext>();

            if (!dbContext.Roles.Any())
            {
                await SeedRoles(userManager, roleManager);
            }

            await next(context);
        }

        private async Task SeedRoles(UserManager<ShareTravelSystemUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));
        }
    }
}