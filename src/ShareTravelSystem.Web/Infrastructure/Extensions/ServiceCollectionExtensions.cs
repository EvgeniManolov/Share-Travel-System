﻿namespace ShareTravelSystem.Web.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Services.Contracts;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            var types = Assembly.GetAssembly(typeof(IService)).GetTypes();


            types
                .Where(t => t.IsClass && !t.IsAbstract && types.Any(s =>
                                s.IsInterface && s.IsAssignableFrom(t) && s.Name.ToLower().Contains(t.Name.ToLower())))
                .Select(t => new
                {
                    Interface = types
                        .FirstOrDefault(i => i.IsAssignableFrom(t) && i.IsInterface),
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