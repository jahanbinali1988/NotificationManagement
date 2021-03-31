using Common.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCommonAspNetCore(this IServiceCollection collection)
        {
            collection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return collection;
        }
        public static void AddCqrsConvention(this MvcOptions options)
        {
            options.Conventions.Add(new CqrsConvention());
        }
    }
}
