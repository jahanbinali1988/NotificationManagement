using Common.Application;
using Common.MongoDb;
using Common.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NotificationManagement.Application.Service;
using NotificationManagement.Application.Service.Contract;
using NotificationManagement.Domain.Models.Message;
using NotificationManagement.Domain.Models.User;
using NotificationManagement.Domain.Services;
using NotificationManagement.Persistence.Mongo.Repositories;
using NotificationManagement.Persistence.Mongo.Services;

namespace NotificationManagement.Config
{
    public class NotificationConfig
    {
        private readonly IConfiguration _configuration;

        public NotificationConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Register(IServiceCollection service)
        {

            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IMessageRepository, MessageRepository>();

            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IMessageService, MessageService>();

            service.AddScoped<IMessageValidator, MessageValidator>();
            service.AddScoped<IUserValidator, UserValidator>();
            service.AddSingleton(a => CreateMongoDb());
        }
        private IMongoDatabase CreateMongoDb()
        {
            var mongoConfigSection = _configuration.GetSection("MongoDb");

            var config = mongoConfigSection.Get<MongoDbConfig>();

            var client = new MongoClient(config.ConnectionString);

            return client.GetDatabase(config.DatabaseName);
        }
    }
}
