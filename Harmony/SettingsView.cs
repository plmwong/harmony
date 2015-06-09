using System;
using System.Windows.Forms;
using Harmony.Properties;
using MetroFramework.Forms;

namespace Harmony
{
    public partial class SettingsView : MetroForm, ISettingsView
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Resizable = false;

            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = string.Format("{0} - Settings", Resources.ApplicationName);

            ToolTip.For(labelCalendarId, Resources.ToolTip_CalendarId);
            ToolTip.For(labelSyncInterval, Resources.ToolTip_SyncInterval);

            var handler = LoadEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            textCalendarId.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Hide();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public event LoadEventHandler LoadEvent;
        public event SaveEventHandler SaveEvent;

        public string GoogleCalendarId
        {
            get { return textCalendarId.Text; }
            set { textCalendarId.Text = value; }
        }

        public TimeSpan SynchronisationInterval
        {
            get { return TimeSpan.FromMinutes(trackbarSyncInterval.Value); }
            set { trackbarSyncInterval.Value = (int) value.TotalMinutes; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            var handler = SaveEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }

            Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void trackbarSyncInterval_ValueChanged(object sender, EventArgs e)
        {
            labelIntervalValue.Text = SynchronisationInterval.ToString();
        }
    }
}