using System;
using System.Threading.Tasks;
using Common.Application.Contract;
using Common.Core;

namespace Common.Application
{
    public class TransactionalCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> _commandHandler;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionalCommandHandlerDecorator(ICommandHandler<T> commandHandler, IUnitOfWork unitOfWork)
        {
            _commandHandler = commandHandler;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(T command)
        {
            try
            {
                await _unitOfWork.Begin();
                await _commandHandler.Handle(command);
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBack();
            }
        }
    }
}
