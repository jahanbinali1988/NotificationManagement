using Komodo.Caching.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CacheHelper
{
    public class CacheHelper : ICacheHelper
    {
        private readonly ICacheProvider[] _providers;
        private readonly ILogger<CacheHelper> _logger;

        public CacheHelper(IEnumerable<ICacheProvider> providers, ILogger<CacheHelper> logger)
        {
            _providers = providers.ToArray();
            _logger = logger;
        }

        public async Task<T> FetchAsync<T>(
            string cacheKey,
            Func<Task<T>> getObjectFromStorageFunc,
            TimeSpan reconstructTime,
            TimeSpan cacheExpiration,
            TimeSpan ttl)
        {
            CacheItem<T> cacheObject = null;

            //get all cache providers
            //get object from cache providers order by registration 
            int validCacheProviderIndex = 0;
            for (var index = 0; index < _providers.Length; index++)
            {
                var cacheProvider = _providers[index];
                try
                {
                    cacheObject = await cacheProvider.FetchAsync<T>(cacheKey, reconstructTime)
                        .ConfigureAwait(false);

                    if (cacheObject != null && !cacheObject.IsExpired)
                    {
                        validCacheProviderIndex = index;
                        break;
                    }

                }
                catch (Exception e)
                {
                    e.Data.Add("CacheType", typeof(T));
                    e.Data.Add("CacheProvider", _providers[index].GetType());
                    _logger.LogError(e, "Unable to fetch data from cache provider");
                }
            }

            //refresh expired cacheProviders 
            if (cacheObject != null && !cacheObject.IsExpired && validCacheProviderIndex != 0)
            {
                TimeSpan? expiration =
                    cacheObject.Expiration.HasValue
                        ? cacheObject.Expiration.Value.Subtract(DateTime.Now)
                        : default(TimeSpan?);

                for (int i = 0; i < validCacheProviderIndex; i++)
                {
                    try
                    {
                        await _providers[i]
                              .StoreAsync(cacheKey, cacheObject.Data, expiration, ttl).ConfigureAwait(false); ;
                    }
                    catch (Exception e)
                    {
                        e.Data.Add("CacheType", typeof(T));
                        e.Data.Add("CacheProvider", _providers[i].GetType());
                        _logger.LogError(e, "Unable to refresh data from cache provider");
                    }

                }

            }

            //refresh all cache providers
            if (cacheObject == null || cacheObject.IsExpired)
            {
                try
                {
                    var result = await getObjectFromStorageFunc().ConfigureAwait(false);

                    for (int i = 0; i < _providers.Length; i++)
                    {

                        var calculatedExpiration = TimeSpan.FromSeconds(((i * 2) + 1) * cacheExpiration.TotalSeconds);

                        try
                        {
                            await _providers[i].StoreAsync(
                                key: cacheKey,
                                value: result,
                                expiration: calculatedExpiration,
                                ttl: ttl
                                ).ConfigureAwait(false);
                        }
                        catch (Exception e)
                        {
                            e.Data.Add("CacheType", typeof(T));
                            e.Data.Add("CacheProvider", _providers[i].GetType());
                            _logger.LogError(e, "Unable to store data from cache provider");
                        }
                    }

                    return result;
                }
                catch (Exception e)
                {
                    e.Data.Add("CacheType", typeof(T));
                    _logger.LogError(e, "Unable to store data from cache provider");
                }
            }

            return cacheObject != null ? cacheObject.Data : await getObjectFromStorageFunc().ConfigureAwait(false);

        }

        public Task RemoveCacheAsync(string cacheKey, TimeSpan? expiration)
        {
            var tasks = _providers.Select(x => x.RemoveAsync(cacheKey, expiration));

            return Task.WhenAll(tasks);
        }

        public Task RemoveCacheAsync(string prefix, object cacheKey, TimeSpan? expiration)
        {
            return RemoveCacheAsync($"{prefix}-{GenerateKey(cacheKey)}", expiration);
        }

        public Task RemoveCacheAsync(string prefix, string cacheKey, TimeSpan? expiration)
        {
            return RemoveCacheAsync($"{prefix}-{cacheKey}", expiration);
        }

        public Task RemoveCacheAsync(string prefix, Guid cacheKey, TimeSpan? expiration)
        {
            return RemoveCacheAsync($"{prefix}-{cacheKey}", expiration);
        }

        public async Task<T> FetchAsync<T>(string prefix, object cacheKey, Func<Task<T>> getObjectFromStorageFunc, TimeSpan reconstructTime,
            TimeSpan cacheExpiration, TimeSpan ttl)
        {
            return await FetchAsync($"{prefix}-{GenerateKey(cacheKey)}", getObjectFromStorageFunc, reconstructTime,
                cacheExpiration, ttl);
        }

        public async Task<T> FetchAsync<T>(string prefix, Guid cacheKey, Func<Task<T>> getObjectFromStorageFunc, TimeSpan reconstructTime,
            TimeSpan cacheExpiration, TimeSpan ttl)
        {
            return await FetchAsync($"{prefix}-{cacheKey}", getObjectFromStorageFunc, reconstructTime,
                cacheExpiration, ttl);
        }

        public async Task<T> FetchAsync<T>(string prefix, string cacheKey, Func<Task<T>> getObjectFromStorageFunc, TimeSpan reconstructTime,
            TimeSpan cacheExpiration, TimeSpan ttl)
        {
            return await  FetchAsync($"{prefix}-{cacheKey}", getObjectFromStorageFunc, reconstructTime,
                cacheExpiration, ttl);
        }

        public async Task StoreAsync<T>(string key, T value, TimeSpan? expiration, TimeSpan? ttl)
        {
            for (int i = 0; i < _providers.Length; i++)
            {
                var provider = _providers[i];

                await provider.StoreAsync(key, value, expiration, ttl);
            }

        }

        private string GenerateKey(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return GenerateMd5String(json);
        }

        private static string GenerateMd5String(string value)
        {
            var sb = new StringBuilder();

            foreach (Byte b in GenerateMd5(value))
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        private static byte[] GenerateMd5(string value)
        {
            using var hash = MD5.Create();
            var enc = Encoding.UTF8;
            return hash.ComputeHash(enc.GetBytes(value));
        }
    }
}