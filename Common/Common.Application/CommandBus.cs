﻿using System.Linq;
using System.Threading.Tasks;
using Common.Application.Contract;

namespace Common.Application
{
    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerResolver _resolver;

        public CommandBus(ICommandHandlerResolver resolver)
        {
            _resolver = resolver;
        }
        public async Task Dispatch<T>(T command) where T :class, ICommand
        {
            var handlers = _resolver.ResolveHandlers(command).ToList();
            foreach (var handler in handlers)
            {
                await handler.Handle(command);
            }
        }
    }
}
