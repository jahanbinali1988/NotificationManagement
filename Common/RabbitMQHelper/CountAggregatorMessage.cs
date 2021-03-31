using System;

namespace RabbitMQHelper
{
    public abstract class CountAggregatorMessage : Message
    {
        public Guid Key { get; set; }

        public int Count { get; set; }
    }
}