using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Harmony
{
    public static class NotifyIconExtensions
    {
        public static void SetTrayIcon(this NotifyIcon notifyIcon, string iconName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var iconStream = assembly.GetManifestResourceStream(string.Format("Harmony.{0}.ico", iconName)))
            {
                Debug.Assert(iconStream != null);

                notifyIcon.Icon = new Icon(iconStream, 40, 40);
            }
        }
    }
}
