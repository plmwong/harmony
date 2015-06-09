using Google.Apis.Calendar.v3.Data;

namespace Harmony.Core.Calendars
{
    public interface IGoogleCalendar : IReadCalendar<Event>, IWriteCalendar<Event>
    {
        string CalendarId { get; }
    }
}