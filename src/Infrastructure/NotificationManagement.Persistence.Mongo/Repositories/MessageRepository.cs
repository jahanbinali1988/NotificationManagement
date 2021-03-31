using System;
using System.Threading.Tasks;
using Common.MongoDb;
using MongoDB.Driver;
using NotificationManagement.Domain.Models.Message;

namespace NotificationManagement.Persistence.Mongo.Repositories
{
    public class MessageRepository : MongoDbRepository<Guid, Message>, IMessageRepository
    {
        public MessageRepository(IMongoDatabase database) : base(database)
        {
        }

        public override async Task<Guid> GetNextId()
        {
            return await Task.FromResult(Guid.NewGuid());
        }
    }
}
