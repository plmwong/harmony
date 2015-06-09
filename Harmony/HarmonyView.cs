using System;
using System.Windows.Forms;
using Harmony.Properties;

namespace Harmony
{
    public class HarmonyView : Form, IHarmonyView
    {
        private readonly NotifyIcon _trayIcon;
        private MenuItem _syncMenuItem;

        public event SyncEventHandler SyncEvent;
        public event SettingsEventHandler SettingsEvent;

        public HarmonyView()
        {
            ContextMenu trayMenu = BuildTrayMenu();
            _trayIcon = BuildTrayIcon(trayMenu);
        }

        private ContextMenu BuildTrayMenu()
        {
            var trayMenu = new ContextMenu();

            trayMenu.MenuItems.Add(Resources.ContextMenu_Sync, OnSync);
            _syncMenuItem = trayMenu.MenuItems[0];

            trayMenu.MenuItems.Add(Resources.ContextMenu_Settings, OnSettings);
            trayMenu.MenuItems.Add(Resources.ContextMenu_Exit, OnExit);

            return trayMenu;
        }

        private NotifyIcon BuildTrayIcon(ContextMenu trayMenu)
        {
            var trayIcon = new NotifyIcon
            {
                Text = Resources.ApplicationName,
                ContextMenu = trayMenu,
                Visible = true
            };

            trayIcon.SetTrayIcon(HarmonyIcons.Normal);

            return trayIcon;
        }

        protected override void OnLoad(EventArgs e)
        {
            HideMainForm();
            base.OnLoad(e);
        }

        private void HideMainForm()
        {
            Visible = false;
            ShowInTaskbar = false;
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnSync(object sender, EventArgs e)
        {
            var handler = SyncEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnSettings(object sender, EventArgs e)
        {
            var handler = SettingsEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void SyncStarted()
        {
            _syncMenuItem.Enabled = false;
            _syncMenuItem.Text = Resources.ContextMenu_SyncInProgress;

            _trayIcon.Text = string.Format("{0} | Syncing...", Resources.ApplicationName);
            _trayIcon.SetTrayIcon(HarmonyIcons.Syncing);
        }

        public void SyncStopped(DateTime timeSyncCompleted, int numberCreated, int numberDeleted)
        {
            _syncMenuItem.Enabled = true;
            _syncMenuItem.Text = Resources.ContextMenu_Sync;

            _trayIcon.Text = string.Format("{0} | Last Synced: {1} | {2} Added, {3} Deleted", Resources.ApplicationName, timeSyncCompleted.ToString("G"), numberCreated, numberDeleted);
            _trayIcon.SetTrayIcon(HarmonyIcons.Normal);
        }

        public void SyncFailed()
        {
            _syncMenuItem.Enabled = true;
            _syncMenuItem.Text = Resources.ContextMenu_Sync;

            _trayIcon.Text = string.Format("{0} | Failed to sync.", Resources.ApplicationName);
            _trayIcon.SetTrayIcon(HarmonyIcons.Failed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _trayIcon.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
