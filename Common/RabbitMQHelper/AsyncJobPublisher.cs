using System;
using EasyNetQ;
using System.Threading.Tasks;
using EasyNetQ.Scheduling;

namespace RabbitMQHelper
{
    public class AsyncJobProducer<TMessage> : IAsyncJobProducer<TMessage>
        where TMessage : Message, new()
    {
        private readonly IBus _bus;

        public AsyncJobProducer(IBus bus)
        {
            _bus = bus;
        }

        public Task PublishAsync(TMessage body, byte priority = 0, TimeSpan? delay = null)
        {
            if (delay != null && delay != default(TimeSpan))
                return _bus.FuturePublishAsync(delay.Value, body);

            return _bus.PublishAsync(body, x =>
            {
                x.WithPriority(priority);
            });
        }

        public void Publish(TMessage body, byte priority = 0, TimeSpan? delay = null)
        {
            if (delay != null && delay != default(TimeSpan))
                _bus.FuturePublish(delay.Value, body);
            else
                _bus.Publish(body, x =>
               {
                   x.WithPriority(priority);
               });
        }

        public void Dispose()
        {

        }
    }
}