using System;

namespace Common.Core.Events.Handlers
{
    public class ActionEventHandler<T> : IEventHandler<T> where T : IEvent
    {
        private readonly Action<T> _action;

        public ActionEventHandler(Action<T> action)
        {
            _action = action;
        }
        public void Handle(T @event)
        {
            _action.Invoke(@event);
        }
    }
}
