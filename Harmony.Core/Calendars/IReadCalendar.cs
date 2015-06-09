using System;
using System.Collections.Generic;

namespace Harmony.Core.Calendars
{
    public interface IReadCalendar<out TCalendarItem> : ICalendar
    {
        IEnumerable<TCalendarItem> GetEntriesInRange(DateTime startDate, DateTime endDate);
    }
}