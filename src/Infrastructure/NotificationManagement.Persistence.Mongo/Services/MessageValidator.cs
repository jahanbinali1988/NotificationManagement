using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using NotificationManagement.Domain.Models.Message;
using NotificationManagement.Domain.Services;

namespace NotificationManagement.Persistence.Mongo.Services
{
    public class MessageValidator : IMessageValidator
    {
        private readonly IMongoDatabase _database;

        public MessageValidator(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<bool> IsTooLong(string content)
        {
            var isTooLong = Task.Run(() => content.Length > 50);
            return await isTooLong;
        }
    }
}
