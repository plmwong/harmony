using System;
using Harmony.Core.OAuth;

namespace Harmony
{
    public class SettingsPresenter : ISettingsPresenter
    {
        private readonly ISettingsView _view;
        private readonly IGoogleConfiguration _googleConfiguration;
        private readonly IHarmonyConfiguration _meshConfiguration;

        public SettingsPresenter(ISettingsView settingsView, IGoogleConfiguration googleConfiguration, IHarmonyConfiguration meshConfiguration)
        {
            _view = settingsView;
            _googleConfiguration = googleConfiguration;
            _meshConfiguration = meshConfiguration;
            _view.LoadEvent += LoadSettings;
            _view.SaveEvent += SaveSettings;

            _view.Hide();
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void LoadSettings(object sender, EventArgs e)
        {
            LoadSettings();
        }

        public ISettingsView View
        {
            get { return _view; }
        }

        public void LoadSettings()
        {
            _view.GoogleCalendarId = _googleConfiguration.GoogleCalendarId;
            _view.SynchronisationInterval = _meshConfiguration.SynchronisationInterval;
        }

        public void SaveSettings()
        {
            _googleConfiguration.GoogleCalendarId = _view.GoogleCalendarId;
            _meshConfiguration.SynchronisationInterval = _view.SynchronisationInterval;
        }

        public void Show()
        {
            _view.Show();
        }
    }
}