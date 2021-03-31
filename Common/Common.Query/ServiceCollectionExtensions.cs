using System.Linq;
using System.Reflection;
using Common.Query.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Query
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCommonQuery(this IServiceCollection collection)
        {
            collection.AddScoped<IQueryBus, QueryBus>();
            collection.AddScoped<IQueryHandlerResolver, AspNetCoreQueryHandlerResolver>();
            return collection;
        }
        public static void RegisterQueryHandlers(this IServiceCollection collection, Assembly assembly)
        {
            collection.Scan(scan =>
                scan.FromAssemblies(assembly)
                    .AddClasses(handlerClass => handlerClass.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
        }
    }
}
