namespace ShareTravelSystem.Web.Infrastructure.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Reflection;
    using ShareTravelSystem.Services.Contracts;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            Type[] types = Assembly.GetAssembly(typeof(IService)).GetTypes();


            types
                .Where(t => t.IsClass && !t.IsAbstract && types.Any(s => s.IsInterface && s.IsAssignableFrom(t) && s.Name.ToLower().Contains(t.Name.ToLower())))
                .Select(t => new
                {
                    Interface = types
                .Where(i => i.IsAssignableFrom(t) && i.IsInterface)
                .FirstOrDefault(),
                    Implementation = t
                })
                .ToDictionary(k => k.Interface, k => k.Implementation).ToList().ForEach(s =>
                {
                    services.AddTransient(s.Key, s.Value);

                });

            return services;
        }
    }
}
