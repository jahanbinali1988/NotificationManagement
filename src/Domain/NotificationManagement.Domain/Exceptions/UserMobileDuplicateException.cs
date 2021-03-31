using Common.Core.Exceptions;

namespace NotificationManagement.Domain.Exceptions
{
    public class UserMobileDuplicateException : BusinessException
    {
        public UserMobileDuplicateException() : base(ExceptionCodes.IsDuplicateMobile, NotificationExceptionCode.IsDuplicateMobile)
        {

        }
    }
}
