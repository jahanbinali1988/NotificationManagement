using Common.Core.Exceptions;

namespace NotificationManagement.Domain.Exceptions
{
    public class TooLongMessageException : BusinessException
    {
        public TooLongMessageException() : base(ExceptionCodes.TooLongMessage, NotificationExceptionCode.TooLongMessage)
        {

        }
    }
}
