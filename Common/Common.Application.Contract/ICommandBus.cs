using System.Threading.Tasks;

namespace Common.Application.Contract
{
    public interface ICommandBus
    {
        Task Dispatch<T>(T command) where T : class, ICommand;
    }
}
