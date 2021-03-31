using System;
using Common.Core.Events.Handlers;

namespace Common.Core.Events
{
    public interface IEventListener
    {
        void Subscribe<T>(Action<T> action) where T : IDomainEvent;
        void Subscribe<T>(IEventHandler<T> @event) where T : IDomainEvent;
    }
}
