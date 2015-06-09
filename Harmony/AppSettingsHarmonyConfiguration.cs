using System;
using System.Configuration;
using Harmony.Core.OAuth;

namespace Harmony
{
    public class AppSettingsHarmonyConfiguration : IHarmonyConfiguration, IGoogleConfiguration
    {
        #region Implementation of IGoogleConfiguration

        public string GoogleRefreshToken
        {
            get { return ConfigurationManager.AppSettings["Google.OAuth.RefreshToken"]; }

            set { ChangeAppSetting("Google.OAuth.RefreshToken", value); }
        }

        public string GoogleCalendarId
        {
            get { return ConfigurationManager.AppSettings["Google.CalendarToSync.Id"]; }

            set { ChangeAppSetting("Google.CalendarToSync.Id", value); }
        }

        #endregion

        #region Implementation of IHarmonyConfiguration

        public TimeSpan SynchronisationInterval
        {
            get { return TimeSpan.Parse(ConfigurationManager.AppSettings["Harmony.SynchronisationInterval"]); }

            set { ChangeAppSetting("Harmony.SynchronisationInterval", value.ToString()); }
        }

        public int PastWeeksToSynchronise
        {
            get { return int.Parse(ConfigurationManager.AppSettings["Harmony.PastWeeksToSynchronise"]); }
        }

        public int FutureWeeksToSynchronise
        {
            get { return int.Parse(ConfigurationManager.AppSettings["Harmony.FutureWeeksToSynchronise"]); }
        }

        private void ChangeAppSetting(string key, string newValue)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = newValue;
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        #endregion
    }
}