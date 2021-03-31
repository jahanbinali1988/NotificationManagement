using Common.Core.Clock;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCommonCore(this IServiceCollection collection)
        {
            collection.AddSingleton<IDateTimeOffset, SystemDateTimeOffset>();
            return collection;
        }
    }
}
