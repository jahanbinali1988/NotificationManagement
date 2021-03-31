using EasyNetQ;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace RabbitMQHelper
{
    public abstract class BaseAsyncCounterAggregator<T> : IAsyncCounter<T>
        where T : CountAggregatorMessage, new()
    {
        private readonly IBus _bus;
        private readonly string _subscriptionId;
        private readonly ConcurrentDictionary<Guid, int> _dictionary;
        private readonly Timer _timer;

        protected abstract TimeSpan PersistInterval { get; }

        protected BaseAsyncCounterAggregator(IBus bus, string subscriptionId)
        {
            _bus = bus;
            _subscriptionId = subscriptionId;
            _dictionary = new ConcurrentDictionary<Guid, int>();
            _timer = new Timer(DoWork, null, dueTime: PersistInterval, period: PersistInterval);

            _bus.Subscribe<T>(_subscriptionId, x =>
            {
                _dictionary.AddOrUpdate(x.Key, x.Count, (key, oldValue) => oldValue + x.Count);
            });
        }

        public void Start()
        {
            _timer.Change(TimeSpan.Zero, PersistInterval);
        }

        public void Stop()
        {
            _timer.Change(TimeSpan.MaxValue, TimeSpan.MaxValue);
            _timer.Dispose();
        }


        private void DoWork(object state)
        {
            //todo: cleanup dictionary
            foreach (var item in _dictionary)
            {
                if (item.Value != 0)
                {
                    if (_dictionary.TryRemove(item.Key, out int value))
                        Persist(item.Key, value);
                }

            }
        }

        protected abstract void Persist(Guid itemKey, int itemValue);

    }
}