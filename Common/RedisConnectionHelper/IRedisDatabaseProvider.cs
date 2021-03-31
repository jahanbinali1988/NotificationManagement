using StackExchange.Redis;

namespace RedisConnectionHelper
{
    public interface IRedisDatabaseProvider
    {
        IDatabase GetDatabase();
    }
}