using System.Threading.Tasks;

namespace Common.Application.Contract
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}