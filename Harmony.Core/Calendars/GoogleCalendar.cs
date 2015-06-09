using System;
using System.Collections.Generic;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Harmony.Core.Infrastructure;

namespace Harmony.Core.Calendars
{
	public class GoogleCalendar : IGoogleCalendar
	{   
		private readonly string _calendarId;
	    private readonly CalendarService _calendarService;
	    
		public GoogleCalendar(string calendarId, CalendarService calendarService)
		{
            if (string.IsNullOrEmpty(calendarId))
			{
				throw new ArgumentException("calendarId cannot be empty or null", "calendarId");
			}

			_calendarId = calendarId;
			_calendarService = calendarService;
		}

		public string CalendarId 
		{
			get { return _calendarId; }
		}

		public IEnumerable<Event> GetEntriesInRange(DateTime startDate, DateTime endDate)
		{
		    string pageToken = null;

		    do
		    {
		        var listRequest = _calendarService.Events.List(_calendarId);

		        listRequest.PageToken = pageToken;
		        listRequest.TimeMin = startDate.ToGoogleFormat();
		        listRequest.TimeMax = endDate.ToGoogleFormat();

		        var request = listRequest.Fetch();

		        pageToken = request.NextPageToken;

		        if (request.IsNotNull() && request.Items.IsNotNull())
		        {
		            foreach (var @event in request.Items)
		            {
		                yield return @event;
		            }
		        }
            } while (pageToken != null);
		}
		
        public void DeleteEntry(Event @event)
        {
			_calendarService.Events
							.Delete(_calendarId, @event.Id)
							.Fetch();
        }		
		
		public void AddEntry(Event @event)
		{
			_calendarService.Events
							.Insert(@event, _calendarId)
							.Fetch();
		}
	}
}
