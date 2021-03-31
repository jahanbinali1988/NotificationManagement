using System;
using System.Collections.Concurrent;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectPoolHelper
{
    public class AsyncObjectPool<T>
    {
        private readonly BlockingCollection<T> _collection;
        private readonly Func<T> _generateMethod;
        private readonly int? _maxPoolSize;
        private int _totalObject = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generateMethod">The method which create a new instance of object</param>
        /// <param name="maxPoolSize">The max count of the instance,and will throw exception when acquired instance more than the limited </param>
        /// <exception cref="ConstraintException"></exception>
        public AsyncObjectPool(Func<T> generateMethod, int maxPoolSize)
        {
            if (maxPoolSize < 1)
                throw new ArgumentOutOfRangeException(nameof(maxPoolSize), $"The {nameof(maxPoolSize)} must bigger than '1'");

            _collection = new BlockingCollection<T>();
            _generateMethod = generateMethod;
            _maxPoolSize = maxPoolSize;
        }

        /// <summary>
        /// Acquire an instance from pool with with specific waiting time
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="token"></param>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>An instance from pool</returns>
        public Task<T> AcquireAsync(TimeSpan timeout, CancellationToken token)
        {
            return Task.Run(() =>
            {
                // Were we already canceled?
                token.ThrowIfCancellationRequested();

                if (_collection.TryTake(out T obj, timeout))
                {
                    if (token.IsCancellationRequested)
                    {
                        _collection.Add(obj);

                        // Clean up here, then...
                        token.ThrowIfCancellationRequested();
                    }
                    return obj;
                }

                //if MaxPoolSize!=null,We care limit
                if (_maxPoolSize.HasValue && _totalObject >= _maxPoolSize)
                    throw new ConstraintException("Unable to acquire object from pool,All the resource in used");


                T newObject = _generateMethod();

                Interlocked.Increment(ref _totalObject);

                return newObject;
            }, token);

        }

        /// <summary>
        /// Release acquired instance 
        /// </summary>
        /// <param name="obj"></param>
        public void Release(T obj)
        {
            _collection.Add(obj);
        }


    }
}