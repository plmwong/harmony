namespace Harmony.Core.Calendars
{
    public interface IGoogleCalendarFactory
    {
        GoogleCalendar Create();
        GoogleCalendar Create(string accessToken);
    }
}