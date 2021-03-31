namespace Common.Query.Handlers
{
    public interface IQueryHandlerResolver
    {
        IQueryHandler<TRequest,TResponse> ResolveHandlers<TRequest, TResponse>(TRequest request) where TRequest : IQuery;
    }
}
