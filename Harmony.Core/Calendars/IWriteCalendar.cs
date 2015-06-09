using Google.Apis.Calendar.v3.Data;

namespace Harmony.Core.Calendars
{
    // ReSharper disable once UnusedTypeParameter
    public interface IWriteCalendar<TCalendarItem>
    {
        void DeleteEntry(Event @event);
        void AddEntry(Event @event);
    }
}