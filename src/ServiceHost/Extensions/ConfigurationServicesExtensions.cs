using Common.Application;
using Common.AspNetCore;
using Common.Core;
using Common.Query;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NotificationManagement.Config;
using NotificationManagement.Presentation.Api.Controllers.Messages;
using NotificationManagement.Presentation.Api.Controllers.Users;

namespace ServiceHost.Extensions
{
    public static class ConfigurationServicesExtensions
    {
        public static IServiceCollection RegisterControllers(this IServiceCollection services)
        {
            services.AddMvcCore()
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<MessagesController>());

            services.AddMvcCore()
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<UsersController>());

            services.AddControllers(config => config.AddCqrsConvention()).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "s";
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                    {
                        ProcessDictionaryKeys = true,
                    },
                };
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy(),
                    AllowIntegerValues = true
                });
            });
            return services;
        }

        public static IServiceCollection RegisterNotificationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            new NotificationConfig(configuration).Register(services);
            services.RegisterCommonCore()
                .RegisterCommonAspNetCore()
                .RegisterCommonApplication()
                .RegisterCommonQuery();
            return services;
        }
    }
}
