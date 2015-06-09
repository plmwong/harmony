using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Harmony.Core.Infrastructure;

namespace Harmony
{
    public static class NotifyIconExtensions
    {
        public static void SetTrayIcon(this NotifyIcon notifyIcon, string iconName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var iconFileName = "Harmony.{0}.ico".FormatWith(iconName);

            using (var iconStream = assembly.GetManifestResourceStream(iconFileName))
            {
                if (iconStream == null)
                {
                    throw new InvalidOperationException("Could not find application tray icon '{0}'".FormatWith(iconFileName));
                }

                notifyIcon.Icon = new Icon(iconStream, 40, 40);
            }
        }
    }
}