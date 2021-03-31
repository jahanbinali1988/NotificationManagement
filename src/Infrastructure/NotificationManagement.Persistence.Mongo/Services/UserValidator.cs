using System.Threading.Tasks;
using MongoDB.Driver;
using NotificationManagement.Domain.Models.User;
using NotificationManagement.Domain.Services;

namespace NotificationManagement.Persistence.Mongo.Services
{
    public class UserValidator : IUserValidator
    {
        private readonly IMongoDatabase _database;

        public UserValidator(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<bool> IsDuplicateEmail(string email)
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq(s => s.Email, email);
            return await _database.GetCollection<User>(nameof(User)).FindSync(filter).AnyAsync();
        }

        public async Task<bool> IsDuplicateMobile(string mobile)
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq(s => s.Mobile, mobile);
            return await _database.GetCollection<User>(nameof(User)).FindSync(filter).AnyAsync();
        }
    }
}
