using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationManagement.Domain.Exceptions
{
    public enum ExceptionCodes
    {
        TooLongMessage = 9000,
        IsDuplicateMobile = 9001,
        IsDuplicateEmail = 9002
    }
}
