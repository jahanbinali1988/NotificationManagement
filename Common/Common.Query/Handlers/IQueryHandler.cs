using System.Threading.Tasks;

namespace Common.Query.Handlers
{
    public interface IQueryHandler<in TRequest, TResponse> where TRequest : IQuery
    {
        Task<TResponse> Handle(TRequest request);
    }
}