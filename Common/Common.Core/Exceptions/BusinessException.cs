using System;

namespace Common.Core.Exceptions
{
    public class BusinessException: Exception
    {
        public string Code { get; private set; }
        public string ExceptionMessage { get; private set; }
        protected BusinessException() { }
        public BusinessException(string code, string message)
        {
            this.Code = code;
            this.ExceptionMessage = message;
        }
        public BusinessException(Enum errorCode, string errorMessage)
        {
            this.Code = Enum.GetName(errorCode.GetType(), errorCode);
            this.ExceptionMessage = errorMessage;
        }
        public BusinessException(Guid entityId, string errorMessage)
        {
            this.Code = entityId.ToString();
            this.ExceptionMessage = errorMessage;
        }
    }
}
