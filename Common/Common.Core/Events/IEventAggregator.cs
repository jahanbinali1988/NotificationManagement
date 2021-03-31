using System;
using Common.Core.Events.Handlers;

namespace Common.Core.Events
{
    public interface IEventAggregator
    {
        void Subscribe<T>(Action<T> action) where T : IEvent;
        void Subscribe<T>(IEventHandler<T> @event) where T : IEvent;
        void Publish<T>(T @event) where T : IEvent;
    }
}
