using System;
using System.Collections.Generic;
using Common.Application.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application
{
    public class CommandHandlerResolver : ICommandHandlerResolver
    {
        private readonly IServiceProvider _context;

        public CommandHandlerResolver(IServiceProvider context)
        {
            _context = context;
        }
        public IEnumerable<ICommandHandler<T>> ResolveHandlers<T>(T command) where T : ICommand
        {
            return _context.GetRequiredService<IEnumerable<ICommandHandler<T>>>();
        }
    }

}
