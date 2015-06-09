using System;

namespace Harmony
{
	public delegate void SyncEventHandler(object sender, EventArgs e);
    public delegate void SettingsEventHandler(object sender, EventArgs e);

	public interface IHarmonyView
	{
		event SyncEventHandler SyncEvent;
        event SettingsEventHandler SettingsEvent;
		void SyncStarted();
		void SyncStopped(DateTime timeSyncCompleted, int numberCreated, int numberDeleted);
		void SyncFailed();
	}
}

