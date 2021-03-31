using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace RedisConnectionHelper
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add required service to resolve IDatabase
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationSection"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            services.Configure<RedisConnectionConfiguration>(configurationSection);

            services.AddSingleton<IRedisDatabaseProvider, RedisDatabaseProvider>();

            services.AddTransient<IDatabase>(provider => provider.GetService<IRedisDatabaseProvider>().GetDatabase());


            return services;
        }
    }

}