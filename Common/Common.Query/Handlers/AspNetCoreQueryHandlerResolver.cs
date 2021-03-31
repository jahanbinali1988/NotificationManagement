using System;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Query.Handlers
{
    public class AspNetCoreQueryHandlerResolver : IQueryHandlerResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public AspNetCoreQueryHandlerResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IQueryHandler<TRequest, TResponse> ResolveHandlers<TRequest, TResponse>(TRequest request) where TRequest : IQuery
        {
            return _serviceProvider.GetRequiredService<IQueryHandler<TRequest, TResponse>>();
        }
    }

}
