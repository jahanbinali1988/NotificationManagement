using System;

namespace RabbitMQHelper
{
    public abstract class Message
    {
        public Guid EventId => Guid.NewGuid();
    }
}