using System;
using System.Diagnostics;
using System.Reflection;
using System.Timers;
using log4net;
using Harmony.Core.Calendars;
using Harmony.Core.Infrastructure;
using Harmony.Core.OAuth;

namespace Harmony
{
    public class HarmonyPresenter : IPresenter<IHarmonyView>
    {
        private readonly IHarmonyView _view;
        private readonly ISettingsPresenter _settingsPresenter;
        private readonly IAuthorisationCodeDialogView _authorisationCodeDialogView;
        private readonly IHarmonyConfiguration _meshConfiguration;
        private readonly IOutlookCalendarFactory _outlookCalendarFactory;
        private readonly IGoogleCalendarFactory _googleCalendarFactory;
        private readonly ISynchroniser<IOutlookCalendar, IGoogleCalendar> _synchroniser;
        private readonly IDateTimeProvider _dateTimeProvider;

        private Timer _synchronisationIntervalTimer;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IHarmonyView View
        {
            get { return _view; }
        }

        public IAuthorisationCodeDialogView AuthorisationCodeDialogView
        {
            get { return _authorisationCodeDialogView; }
        }

        public HarmonyPresenter(IHarmonyView view, ISettingsPresenter settingsPresenter, IAuthorisationCodeDialogView authorisationCodeDialogView, IHarmonyConfiguration meshConfiguration,
            IOutlookCalendarFactory outlookCalendarFactory, IGoogleCalendarFactory googleCalendarFactory,
            ISynchroniser<IOutlookCalendar, IGoogleCalendar> synchroniser, IDateTimeProvider dateTimeProvider)
        {
            _view = view;
            _settingsPresenter = settingsPresenter;
            _authorisationCodeDialogView = authorisationCodeDialogView;
            _meshConfiguration = meshConfiguration;

            SubscribeToEvents(_view);
            StartSyncTimer();

            _outlookCalendarFactory = outlookCalendarFactory;
            _googleCalendarFactory = googleCalendarFactory;
            _synchroniser = synchroniser;
            _dateTimeProvider = dateTimeProvider;
        }

        private void StartSyncTimer()
        {
            _synchronisationIntervalTimer = new Timer(_meshConfiguration.SynchronisationInterval.TotalMilliseconds) {AutoReset = true};
            _synchronisationIntervalTimer.Elapsed += SynchronisationIntervalTimerOnElapsed;
            _synchronisationIntervalTimer.Start();
        }

        private void SynchronisationIntervalTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Log.Info("Timer elapsed, starting synchronisation.");
            Synchronise();
        }

        private void SubscribeToEvents(IHarmonyView view)
        {
            view.SyncEvent += OnSync;
            view.SettingsEvent += OnSettings;
        }

        private void OnSettings(object sender, EventArgs e)
        {
            _settingsPresenter.Show();
        }

        private void OnSync(object sender, EventArgs e)
        {
            Log.Info("Manual synchronisation requested, starting synchronisation.");
            Synchronise();
        }

        public async void Synchronise()
        {
            var now = _dateTimeProvider.Now;

            var startDate = now - _meshConfiguration.PastWeeksToSynchronise.Weeks();
            var endDate = now + _meshConfiguration.FutureWeeksToSynchronise.Weeks();

            var outlookCalendar = _outlookCalendarFactory.Create();

            try
            {
                var googleCalendar = _googleCalendarFactory.Create();

                View.SyncStarted();
                var result = await _synchroniser.Synchronise(outlookCalendar, googleCalendar, startDate, endDate);
                View.SyncStopped(_dateTimeProvider.Now, result.NumberCreated, result.NumberDeleted);
            }
            catch (GoogleAuthorizationRequiredException authException)
            {
                Log.Info("User consent required for Google authorisation, requesting access.");
                Process.Start(authException.AuthorisationUri);

                var accessToken = _authorisationCodeDialogView.PromptForAccessToken();

                Synchronise(accessToken, startDate, endDate, outlookCalendar);
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while attempting to synchronise.", ex);
                View.SyncFailed();
            }
        }

        private async void Synchronise(string accessToken, DateTime startDate, DateTime endDate, IOutlookCalendar outlookCalendar)
        {
            try
            {
                var googleCalendar = _googleCalendarFactory.Create(accessToken);

                View.SyncStarted();
                var result = await _synchroniser.Synchronise(outlookCalendar, googleCalendar, startDate, endDate);
                View.SyncStopped(_dateTimeProvider.Now, result.NumberCreated, result.NumberDeleted);
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while attempting to synchronise.", ex);
                View.SyncFailed();
            }
        }
    }
}