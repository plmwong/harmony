using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using log4net;
using Harmony.Core.Infrastructure;
using Microsoft.Office.Interop.Outlook;

namespace Harmony.Core.Calendars
{
    public class OutlookToGoogleSynchroniser : ISynchroniser<IOutlookCalendar, IGoogleCalendar>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<SynchronisationResult> Synchronise(IOutlookCalendar source, IGoogleCalendar dest, DateTime startDate, DateTime endDate)
        {
            Log.InfoFormat("Synchronising Outlook Calendar with Google Calendar '{0}'; For date range between '{1}' and '{2}'", dest.CalendarId, startDate, endDate);

            var outlookEntries = await GetOutlookEntries(source, startDate, endDate);
            var googleEntries = await GetGoogleEntries(dest, startDate, endDate);

            if (googleEntries.Count > outlookEntries.Count)
            {
                Log.Warn("Google Calendar contains more entries than the Outlook Calendar it is supposed to sync with. Looking for duplicate entries within the Google Calendar.");
                var duplicateEntries = await GetDuplicateEntries(googleEntries);

                await DeleteObsoleteEntries(duplicateEntries, dest);

                Log.InfoFormat("{0} duplicates removed from Google Calendar", duplicateEntries.Count);
            }

            Log.InfoFormat("Found {0} total entries in Outlook Calendar".IndentBy(1), outlookEntries.Count);
            Log.InfoFormat("Found {0} total entries in Google Calendar".IndentBy(1), googleEntries.Count);

            var newEntries = await GetNewEntries(outlookEntries, googleEntries);
            var obsoleteEntries = await GetObsoleteEntries(outlookEntries, googleEntries);

            Log.InfoFormat("Found {0} new entries in Outlook Calendar, which are not in Google Calendar, these will be created".IndentBy(1), newEntries.Count);
            Log.InfoFormat("Found {0} obsolete entries in Google Calendar, which are no longer in Outlook Calendar, these will be deleted".IndentBy(1), obsoleteEntries.Count);

            await CreateNewEntries(newEntries, dest);
            await DeleteObsoleteEntries(obsoleteEntries, dest);

            Log.InfoFormat("Synchronisation complete.");

            return new SynchronisationResult(newEntries.Count, obsoleteEntries.Count);
        }

        private Task<IList<AppointmentItem>> GetOutlookEntries(IOutlookCalendar source, DateTime startDate, DateTime endDate)
        {
            return Task.Run<IList<AppointmentItem>>(() => source.GetEntriesInRange(startDate, endDate).ToList());
        }

        private Task<IList<Event>> GetGoogleEntries(IGoogleCalendar dest, DateTime startDate, DateTime endDate)
        {
            return Task.Run<IList<Event>>(() => dest.GetEntriesInRange(startDate, endDate).ToList());
        }

        private Task<IList<AppointmentItem>> GetNewEntries(IEnumerable<AppointmentItem> outlookEntries, IEnumerable<Event> googleEntries)
        {
            return Task.Run<IList<AppointmentItem>>(() =>
            {
                var newEntries = outlookEntries
                    .Where(o => googleEntries.All(g => g.IsNotEquivalentTo(o))).ToList();

                return newEntries;
            });
        }

        private Task<IList<Event>> GetObsoleteEntries(IEnumerable<AppointmentItem> outlookEntries, IEnumerable<Event> googleEntries)
        {
            return Task.Run<IList<Event>>(() =>
            {
                var obsoleteEntries = googleEntries
                    .Where(g => outlookEntries.All(o => o.IsNotEquivalentTo(g)))
                    .Where(g => g.Description == null || !g.Description.Contains("#keep")).ToList();

                return obsoleteEntries;
            });
        }

        private Task<IList<Event>> GetDuplicateEntries(IEnumerable<Event> googleEntries)
        {
            return Task.Run<IList<Event>>(() =>
            {
                var duplicateEvents = new List<Event>();
                var duplicateGroups = googleEntries.GroupBy(e => e.Signature()).Where(g => g.Count() > 1);

                foreach (var duplicateGroup in duplicateGroups)
                {
                    duplicateEvents.AddRange(duplicateGroup.Skip(1));
                }

                return duplicateEvents;
            });
        }

        private Task CreateNewEntries(IEnumerable<AppointmentItem> newEntries, IGoogleCalendar dest)
        {
            return Task.Run(() =>
            {
                foreach (var newEntry in newEntries)
                {
                    Log.DebugFormat("Adding new entry for '{0}' at '{1} - {2}' / {3}".IndentBy(1), newEntry.Subject, newEntry.Start, newEntry.End, newEntry.Location);
                    dest.AddEntry(newEntry.ToEvent());
                }
            });
        }

        private Task DeleteObsoleteEntries(IEnumerable<Event> obosleteEntries, IGoogleCalendar dest)
        {
            return Task.Run(() =>
            {
                foreach (var obsoleteEntry in obosleteEntries)
                {
                    Log.DebugFormat("Deleting entry for '{0}' at '{1} - {2}' / {3}".IndentBy(1), obsoleteEntry.Summary, obsoleteEntry.Start.DateTime, obsoleteEntry.End.DateTime, obsoleteEntry.Location);
                    dest.DeleteEntry(obsoleteEntry);
                }
            });
        }
    }
}

