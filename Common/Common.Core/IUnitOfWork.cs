using System.Threading.Tasks;

namespace Common.Core
{
    public interface IUnitOfWork
    {
        Task Begin();
        Task Commit();
        Task RollBack();
    }
}
