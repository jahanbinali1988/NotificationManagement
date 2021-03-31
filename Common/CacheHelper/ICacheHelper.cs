using System;
using System.Threading.Tasks;

namespace CacheHelper
{
    public interface ICacheHelper
    {
        Task<T> FetchAsync<T>(
            string cacheKey,
            Func<Task<T>> getObjectFromStorageFunc,
            TimeSpan reconstructTime,
            TimeSpan cacheExpiration,
            TimeSpan ttl);

        Task RemoveCacheAsync(string cacheKey, TimeSpan? expiration);

        Task RemoveCacheAsync(string prefix,
            object cacheKey, TimeSpan? expiration);

        Task RemoveCacheAsync(string prefix,
            string cacheKey, TimeSpan? expiration);

        Task RemoveCacheAsync(string prefix,
            Guid cacheKey, TimeSpan? expiration);

        Task<T> FetchAsync<T>(
            string prefix,
            object cacheKey,
            Func<Task<T>> getObjectFromStorageFunc,
            TimeSpan reconstructTime,
            TimeSpan cacheExpiration,
            TimeSpan ttl);


        Task<T> FetchAsync<T>(
            string prefix,
            Guid cacheKey,
            Func<Task<T>> getObjectFromStorageFunc,
            TimeSpan reconstructTime,
            TimeSpan cacheExpiration,
            TimeSpan ttl);

        Task<T> FetchAsync<T>(
            string prefix,
            string cacheKey,
            Func<Task<T>> getObjectFromStorageFunc,
            TimeSpan reconstructTime,
            TimeSpan cacheExpiration,
            TimeSpan ttl);


    }
}