using System;

namespace Common.Core.Clock
{
    public class SystemDateTimeOffset : IDateTimeOffset
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}