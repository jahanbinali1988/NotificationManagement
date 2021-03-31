using System.Threading.Tasks;

namespace Common.Query.Handlers
{
    public class QueryBus : IQueryBus
    {
        private readonly IQueryHandlerResolver _handlerResolver;

        public QueryBus(IQueryHandlerResolver handlerResolver)
        {
            _handlerResolver = handlerResolver;
        }
        public async Task<TResponse> Execute<TRequest, TResponse>(TRequest request) where TRequest : IQuery
        {
            var handler = _handlerResolver.ResolveHandlers<TRequest, TResponse>(request);
            return await handler.Handle(request);
        }
    }
}
