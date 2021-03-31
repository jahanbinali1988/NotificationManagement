using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.Exceptions
{
    public class EntityNotFoundedException : BusinessException
    {
        public EntityNotFoundedException(Guid id, string message) : base(id, message)
        {

        }

    }
}
