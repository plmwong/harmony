using System;
using System.Collections.Generic;
using Harmony.Core.Infrastructure;
using Microsoft.Office.Interop.Outlook;

namespace Harmony.Core.Calendars
{
    public class OutlookCalendar : IOutlookCalendar
    {
		private readonly MAPIFolder _outlookCalendarFolder;
        
		public OutlookCalendar(MAPIFolder outlookCalendarFolder)
        {
			_outlookCalendarFolder = outlookCalendarFolder;
        }
        
		public IEnumerable<AppointmentItem> GetEntriesInRange(DateTime startDate, DateTime endDate)
        {
			var outlookCalendarItems = _outlookCalendarFolder.Items;

			outlookCalendarItems.Sort("[Start]", Type.Missing);
			outlookCalendarItems.IncludeRecurrences = true;
            
			if (outlookCalendarItems.IsNotNull())
            {
				var filter = @"[End] >= '{0}' AND [Start] < '{1}'".FormatWith(startDate.ToGeneralFormat(), endDate.ToGeneralFormat());
                
				foreach(AppointmentItem appointmentItem in outlookCalendarItems.Restrict(filter))
                {
                    yield return appointmentItem;
                }
            }
        }
    }
}
