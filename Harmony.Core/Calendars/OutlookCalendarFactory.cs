using Microsoft.Office.Interop.Outlook;

namespace Harmony.Core.Calendars
{
    public class OutlookCalendarFactory : IOutlookCalendarFactory
    {
        public OutlookCalendar Create()
        {
            var outlookApplication = new Application();
            NameSpace outlookNamespace = outlookApplication.GetNamespace(@"mapi");

            outlookNamespace.Logon(string.Empty, string.Empty, true, true);
            var calendarFolder = outlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
            outlookNamespace.Logoff();

            return new OutlookCalendar(calendarFolder);
        }
    }
}

