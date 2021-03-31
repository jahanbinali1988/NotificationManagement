using EasyNetQ;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RabbitMQHelper
{
    public abstract class BaseAsyncJobConsumer<TMessage> : IAsyncJobConsumer<TMessage>
        where TMessage : Message, new()
    {
        private readonly IBus _bus;
        private readonly string _subscriptionId;
        private readonly List<ISubscriptionResult> _subscriptionResults;
        private readonly ILogger _logger;

        protected BaseAsyncJobConsumer(IBus bus, ILogger logger, string subscriptionId = "")
        {
            _bus = bus;
            _logger = logger;
            _subscriptionId = subscriptionId;
            _subscriptionResults = new List<ISubscriptionResult>();
        }

        public void Subscribe()
        {
            _subscriptionResults.Add(_bus.SubscribeAsync<TMessage>(
                _subscriptionId, OnMessageWrapper));
        }

        public async Task OnMessageWrapper(TMessage message)
        {
            try
            {
                await OnMessage((message));
            }
            catch (Exception e)
            {
                e.Data.Add("MessageType", typeof(TMessage));
                _logger.LogCritical(e, e.Message ?? "Throw unknown exception when processing OnMessage event");
                throw;
            }
        }

        public abstract Task OnMessage(TMessage message);

        public void Dispose()
        {
            _subscriptionResults.ForEach(x => x.Dispose());
        }
    }
}