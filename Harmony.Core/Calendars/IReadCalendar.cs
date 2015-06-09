using System;
using System.Collections.Generic;

namespace Harmony.Core.Calendars
{
	public interface IReadCalendar<TCalendarItem> : ICalendar
	{
		IEnumerable<TCalendarItem> GetEntriesInRange(DateTime startDate, DateTime endDate);
	}
}

