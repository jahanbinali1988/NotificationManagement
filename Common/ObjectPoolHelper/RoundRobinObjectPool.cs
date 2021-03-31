using System;
using System.Collections.Concurrent;
using System.Data;
using System.Threading;

namespace ObjectPoolHelper
{
    /// <summary>
    /// Provide object pooling for object that we want to create limited
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RoundRobinObjectPool<T>
    {
        private readonly ConcurrentQueue<T> _collection;
        private readonly Func<T> _generateMethod;
        private readonly int? _maxPoolSize;
        private int _totalObject = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generateMethod">The method which create a new instance of object</param>
        public RoundRobinObjectPool(Func<T> generateMethod)
        {

            _collection = new ConcurrentQueue<T>();
            _generateMethod = generateMethod;
            _maxPoolSize = default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generateMethod">The method which create a new instance of object</param>
        /// <param name="maxPoolSize">The max count of the instance,and will throw exception when acquired instance more than the limited </param>
        /// <exception cref="ConstraintException"></exception>
        public RoundRobinObjectPool(Func<T> generateMethod, int maxPoolSize)
        {

            _collection = new ConcurrentQueue<T>();
            _generateMethod = generateMethod;
            _maxPoolSize = maxPoolSize;
        }

        /// <summary>
        /// Acquire an instance from pool
        /// </summary>
        /// <exception cref="ConstraintException"></exception>
        /// <returns></returns>
        public T Acquire()
        {
            if (_collection.TryDequeue(out T obj))
                return obj;

            //if MaxPoolSize!=null,We care limit
            if (_maxPoolSize.HasValue && _totalObject >= _maxPoolSize)
                throw new ConstraintException("Unable to acquire object from pool,All the resource in used");

            T newObject = _generateMethod();
            Interlocked.Increment(ref _totalObject);

            return newObject;
        }

        /// <summary>
        /// Release acquired instance 
        /// </summary>
        /// <param name="obj"></param>
        public void Release(T obj)
        {
            _collection.Enqueue(obj);
        }
    }
}