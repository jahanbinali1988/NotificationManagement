using System;
using System.Collections.Generic;
using System.Text;
using Common.Core.Events;

namespace Common.Domain
{
    public interface IAggregateRoot
    {
        void Publish<T>(T @event) where T : IDomainEvent;
        IReadOnlyCollection<IDomainEvent> GetEvents();
        void ClearEvents();
    }
}
