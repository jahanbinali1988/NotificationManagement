using System;
using System.Threading.Tasks;

namespace RabbitMQHelper
{
    public interface IAsyncJobConsumer<TMessage> : IDisposable
        where TMessage : Message, new()
    {
        void Subscribe();

        Task OnMessage(TMessage arg);
    }
}