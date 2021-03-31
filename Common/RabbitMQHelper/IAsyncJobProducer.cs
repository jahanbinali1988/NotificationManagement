using System;
using System.Threading.Tasks;

namespace RabbitMQHelper
{
    public interface IAsyncJobProducer<TMessage> : IDisposable
        where TMessage : Message, new()
    {
        Task PublishAsync(TMessage body, byte priority = 0,TimeSpan? delay=null);
        void Publish(TMessage body, byte priority = 0, TimeSpan? delay = null);
    }
}