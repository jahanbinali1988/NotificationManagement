using System.Threading.Tasks;

namespace Common.Domain
{
    public interface IRepository
    {
    }
    public interface IRepository<TKey, T> : IRepository 
    {
        Task<TKey> GetNextId();
        Task Create(T aggregate);
        Task Update(T aggregate);
        Task Remove(T aggregate);
        Task<T> Get(TKey key);
    }
}