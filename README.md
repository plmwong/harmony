# Harmony #

Introduction
========
Harmony is a simple system tray utility which periodically synchronises an Outlook Calendar with a Google Calendar. It was written after Google Calendar Sync was announced to become non-operational as of August 1, 2014.

Disclaimer
========
Harmony was written just for my own use, so it only really does what I need it to do. I can't give any guarantees that it will remain functional, or that I can (reliably) maintain it going forward. I am making the source code available under The MIT License, so that it can either be taken and improved, or simply to help other people in making their own syncing apps.

Usage
========
Harmony does not require administrator privileges to run and is a completely self-contained application. It does however require the .NET 4.5 Framework to be installed on the machine. It is also intended to be run on a machine on which you are using your Outlook client (so that Harmony can use the Outlook profile information to read your Outlook calendar).

Configuration
--------
Currently, Harmony does not have a settings dialog (hopefully soon!), but still only has a small number of settings; which are currently controlled in the .config XML file:

* **Google.OAuth.RefreshToken**: Stores the OAuth refresh token to allow Harmony to re-authenticate in future syncs. Initially this is empty, and will be populated as part syncing for the first time. There is no need to change this value at all.
* **Google.CalendarToSync.Id**: The Id of the Google calendar to sync with. This can be found in your Calendar Settings > Calendar Details > Calendar Address. If you only have one calendar on your account, this will often be your Google account e-mail address.

* **Harmony.SynchronisationInterval**: This is how often Harmony will synchronise the two calendars. By default this is set to '00:30:00' (every 30 minutes).
* **Harmony.PastWeeksToSynchronise**: This is how far into the past that Harmony will look for events to sync.
* **Harmony.FutureWeeksToSynchronise**: This is how far into the future that Harmony will look for events to sync.

Authenticating
--------
Harmony uses OAuth to access you Google Calendar. When a sync is first performed, Harmony will require you to allow it to access your Google calendar. A browser window will open to ask you for this access.

Accepting the request will then display an 'Access Token', which you will need to copy and paste into the dialog that Harmony provides.

Once this is done, Harmony will attempt to connect to your Google calendar, and if successful, it will store a 'Refresh Token' into the .config which will allow continued access without re-prompting for another 'Access Token'.

One-Way Sync
--------
Currently, Harmony only provides a one-way synchronisation mechanism which flows from Outlook to Google. Note, that this is a *destructive* sync, which means that any existing calendar entry in the Google Calendar, which is not also in the Outlook Calendar will be deleted. If you use Google Calendar as a personal calendar, it is recommended that you create a brand new calendar for syncing with your Outlook. You can then use the Id for this new calendar as the *Google.CalendarToSync.Id* in the .config for Harmony, so that Harmony will only sync with that calendar and not harm your existing calendars.

Indicators
--------
The colour of the system tray icon indicates the status of Harmony (Blue = idle, Green = sync in progress, Yellow = sync failed). Mousing over the icon will also give some brief information about when the last sync occurred, and how many events were added/deleted.

License
========
The full source code for Harmony is provided under The MIT License (MIT). A quick brief (tl;dr) summary of The MIT License can be found at https://tldrlegal.com/license/mit-license

Attribution
========
Harmony was originally derived from 'OutlookGoogleSync', authored by 'zissakos', and available at:
http://outlookgooglesync.codeplex.com/

Harmony Icon uses "MetroUI Apps Live Sync Icon", created by 'igh0zt (Tim Kang)', and available at:
http://www.iconarchive.com/show/ios7-style-metro-ui-icons-by-igh0zt/MetroUI-Apps-Live-Sync-icon.html
