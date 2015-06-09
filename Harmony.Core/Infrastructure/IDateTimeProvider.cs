using System;

namespace Harmony.Core.Infrastructure
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}