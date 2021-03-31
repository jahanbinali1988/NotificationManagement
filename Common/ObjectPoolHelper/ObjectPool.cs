using System;
using System.Collections.Concurrent;
using System.Data;
using System.Threading;

namespace ObjectPoolHelper
{
    //Object pooling can offer a significant performance boost;
    //It is most effective in situations where the cost of initializing a class instance is high,
    //the rate of instantiation of a class is high, and the number of instantiations in use at any one time is low.
    /// <summary>
    /// Provide object pooling for object that we want to create limited
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPool<T>
    {
        private readonly ConcurrentBag<T> _collection;
        private readonly Func<T> _generateMethod;
        private readonly int? _maxPoolSize;
        private int _totalObject = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generateMethod">The method which create a new instance of object</param>
        public ObjectPool(Func<T> generateMethod)
        {

            _collection = new ConcurrentBag<T>();
            _generateMethod = generateMethod;
            _maxPoolSize = default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generateMethod">The method which create a new instance of object</param>
        /// <param name="maxPoolSize">The max count of the instance,and will throw exception when acquired instance more than the limited </param>
        /// <exception cref="ConstraintException"></exception>
        public ObjectPool(Func<T> generateMethod, int maxPoolSize)
        {

            _collection = new ConcurrentBag<T>();
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
            if (_collection.TryTake(out T obj))
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
            _collection.Add(obj);
        }
    }

    //Object pooling can offer a significant performance boost;
    //It is most effective in situations where the cost of initializing a class instance is high,
    //the rate of instantiation of a class is high, and the number of instantiations in use at any one time is low.
}