using System.Collections.Generic;
using Google.Apis.Calendar.v3.Data;
using Harmony.Core.Infrastructure;
using Microsoft.Office.Interop.Outlook;

namespace Harmony.Core.Calendars
{
	public static class CalendarItemExtensions
	{
	    public static string Signature(this Event @event)
	    {
			string eventStartTimeFormatted = @event.Start.DateTime ?? @event.Start.Date;
			string eventEndTimeFormatted = @event.End.DateTime ?? @event.End.Date;

			var eventStartTime = eventStartTimeFormatted.ToDateTime();
			var eventEndTime = eventEndTimeFormatted.ToDateTime();

            return "event:{0}:{1}:{2}:{3}".FormatWith(@event.Summary, @event.Location, eventStartTime, eventEndTime);
	    }

		public static Event ToEvent(this AppointmentItem appointmentItem)
		{
			var createdEvent = new Event {
				Start = new EventDateTime(),
				End = new EventDateTime(),
				Summary = appointmentItem.Subject,
				Description = appointmentItem.Body,
				Location = appointmentItem.Location
			};

			if (appointmentItem.AllDayEvent)
			{
				createdEvent.Start.Date = appointmentItem.Start.ToGoogleDate();
				createdEvent.End.Date = appointmentItem.End.ToGoogleDate();
			} 
			else 
			{
				createdEvent.Start.DateTime = appointmentItem.Start.ToGoogleFormat();
				createdEvent.End.DateTime = appointmentItem.End.ToGoogleFormat();
			}

			if (appointmentItem.ReminderSet)
			{
				var reminder = new EventReminder {
					Method = @"popup",
					Minutes = appointmentItem.ReminderMinutesBeforeStart
				};

				createdEvent.Reminders = new Event.RemindersData {
					UseDefault = false,
					Overrides = new List<EventReminder> {
						reminder
					}
				};
			}

			return createdEvent;
		}

		public static bool IsEquivalentTo(this Event @event, AppointmentItem appointmentItem)
		{
			string eventStartTimeFormatted = @event.Start.DateTime ?? @event.Start.Date;
			string eventEndTimeFormatted = @event.End.DateTime ?? @event.End.Date;

			var eventStartTime = eventStartTimeFormatted.ToDateTime();
			var eventEndTime = eventEndTimeFormatted.ToDateTime();

			return @event.Summary == appointmentItem.Subject &&
				@event.Location == appointmentItem.Location &&
				@eventStartTime == appointmentItem.Start &&
				@eventEndTime == appointmentItem.End;
		}

		public static bool IsEquivalentTo(this AppointmentItem appointmentItem, Event @event)
		{
			return @event.IsEquivalentTo(appointmentItem);
		}

		public static bool IsNotEquivalentTo(this Event @event, AppointmentItem appointmentItem)
		{
			return !@event.IsEquivalentTo(appointmentItem);
		}

		public static bool IsNotEquivalentTo(this AppointmentItem appointmentItem, Event @event)
		{
			return !@event.IsEquivalentTo(appointmentItem);
		}
	}
}

