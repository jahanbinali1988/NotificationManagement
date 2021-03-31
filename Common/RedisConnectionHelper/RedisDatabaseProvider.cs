using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ObjectPoolHelper;
using StackExchange.Redis;

namespace RedisConnectionHelper
{
    public class RedisDatabaseProvider : IRedisDatabaseProvider
    {
        private readonly RoundRobinObjectPool<ConnectionMultiplexer> _connectionPool;
        private readonly ILogger<RedisDatabaseProvider> _logger;

        public RedisDatabaseProvider(IOptions<RedisConnectionConfiguration> options, ILogger<RedisDatabaseProvider> logger)
        {
            _logger = logger;

            _connectionPool = new RoundRobinObjectPool<ConnectionMultiplexer>(() =>
              {
                  var config = ConfigurationOptions.Parse(options.Value.Connection);
                  config.AbortOnConnectFail = false;
                  config.ConnectTimeout = 1000;
                  config.KeepAlive = 2;
                  config.DefaultDatabase = 0;
                  config.ReconnectRetryPolicy = new ExponentialRetry(1000);
                  return ConnectionMultiplexer.Connect(config);
              }, options.Value.ConnectionCount);

        }

        public IDatabase GetDatabase()
        {
            ConnectionMultiplexer multiplexer = null;

            try
            {
                multiplexer = _connectionPool.Acquire();
                return multiplexer.GetDatabase();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unable to get Connected IDatabase");
                throw;
            }
            finally
            {
                if (multiplexer != null)
                    _connectionPool.Release(multiplexer);
            }
        }
    }
}