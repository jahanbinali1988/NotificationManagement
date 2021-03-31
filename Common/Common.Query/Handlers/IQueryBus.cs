using System.Threading.Tasks;

namespace Common.Query.Handlers
{
    public interface IQueryBus
    {
        Task<TResponse> Execute<TRequest, TResponse>(TRequest request) where TRequest : IQuery;
    }
}
