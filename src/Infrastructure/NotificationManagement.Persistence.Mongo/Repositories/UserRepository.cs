using System;
using System.Threading.Tasks;
using Common.MongoDb;
using MongoDB.Driver;
using NotificationManagement.Domain.Models.User;

namespace NotificationManagement.Persistence.Mongo.Repositories
{
    public class UserRepository : MongoDbRepository<Guid, User>, IUserRepository
    {
        public UserRepository(IMongoDatabase database) : base(database)
        {
        }

        public override async Task<Guid> GetNextId()
        {
            return await Task.FromResult(Guid.NewGuid());
        }
    }
}
