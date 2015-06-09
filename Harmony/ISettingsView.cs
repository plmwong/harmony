using System;

namespace Harmony
{
    public delegate void LoadEventHandler(object sender, EventArgs e);
    public delegate void SaveEventHandler(object sender, EventArgs e);

    public interface ISettingsView
    {
        event LoadEventHandler LoadEvent;
        event SaveEventHandler SaveEvent;

        string GoogleCalendarId { get; set; }
        TimeSpan SynchronisationInterval { get; set; }

        void Show();
        void Hide();
    }
}
