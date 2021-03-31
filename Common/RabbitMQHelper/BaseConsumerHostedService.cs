using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQHelper
{
    public class BaseConsumerHostedService<TMessage> : IHostedService
        where TMessage : Message, new()
    {
        private readonly IAsyncJobConsumer<TMessage> _consumer;

        public BaseConsumerHostedService(IAsyncJobConsumer<TMessage> consumer)
        {
            _consumer = consumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Dispose();

            return Task.CompletedTask;
        }
    }
}