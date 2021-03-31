using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Common.Application.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCommonApplication(this IServiceCollection collection)
        {
            collection.AddScoped<ICommandBus, CommandBus>();
            collection.AddScoped<ICommandHandlerResolver, CommandHandlerResolver>();
            return collection;
        }
        public static void RegisterCommandHandlers(this IServiceCollection collection, Assembly assembly)
        {
            collection.Scan(scan =>
                scan.FromAssemblies(assembly)
                    .AddClasses(handlerClass => handlerClass.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
        }
    }

}
