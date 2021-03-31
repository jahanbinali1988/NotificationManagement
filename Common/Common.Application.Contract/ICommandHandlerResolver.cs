using System.Collections.Generic;

namespace Common.Application.Contract
{
    public interface ICommandHandlerResolver
    {
        IEnumerable<ICommandHandler<T>> ResolveHandlers<T>(T command) where T : ICommand;
    }
}
