using System;

namespace Common.Core.Clock
{
    public interface IDateTimeOffset
    {
        DateTimeOffset Now();
    }
}