namespace Common.Core.Events.Handlers
{
    public interface IEventHandler<in T> where T : IEvent
    {
        public void Handle(T @event);
    }
}
