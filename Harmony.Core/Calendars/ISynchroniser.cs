using System;
using System.Threading.Tasks;

namespace Harmony.Core.Calendars
{
    public interface ISynchroniser<in TSourceCalendar, in TDestCalendar>
        where TSourceCalendar : ICalendar
        where TDestCalendar : ICalendar
    {
        Task<SynchronisationResult> Synchronise(TSourceCalendar source, TDestCalendar dest, DateTime startDate, DateTime endDate);
    }
}