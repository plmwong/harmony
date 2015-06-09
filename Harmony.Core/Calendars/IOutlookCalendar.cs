using Microsoft.Office.Interop.Outlook;

namespace Harmony.Core.Calendars
{
    public interface IOutlookCalendar : IReadCalendar<AppointmentItem>
    {
    }
}