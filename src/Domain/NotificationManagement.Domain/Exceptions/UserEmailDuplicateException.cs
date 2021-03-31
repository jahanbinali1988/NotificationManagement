using Common.Core.Exceptions;

namespace NotificationManagement.Domain.Exceptions
{
    public class UserEmailDuplicateException : BusinessException
    {
        public UserEmailDuplicateException() : base(ExceptionCodes.IsDuplicateEmail, NotificationExceptionCode.IsDuplicateEmail)
        {

        }
    }
}
