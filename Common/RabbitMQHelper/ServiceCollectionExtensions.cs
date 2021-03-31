using EasyNetQ;
using EasyNetQ.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using EasyNetQ.Scheduling;

namespace RabbitMQHelper
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add IBus which using in asyncJob services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="section">Contains the configuration section which contains 'Connection' value</param>
        public static void AddAsyncJobHelper(this IServiceCollection services, IConfigurationSection section)
        {
            services.AddSingleton<IBus>(s => RabbitHutch.CreateBus(
                section.GetValue<string>("Connection"),
                register =>
                {
                    register.Register<IScheduler, DelayedExchangeScheduler>();
                }));
        }
    }

}