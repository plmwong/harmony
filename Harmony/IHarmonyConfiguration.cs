using System;

namespace Harmony
{
    public interface IHarmonyConfiguration
    {
        TimeSpan SynchronisationInterval { get; set; }
        int PastWeeksToSynchronise { get; }
        int FutureWeeksToSynchronise { get; }
    }
}