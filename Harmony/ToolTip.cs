using System.Windows.Forms;
using MetroFramework.Components;

namespace Harmony
{
    public static class ToolTip
    {
        public static MetroToolTip For(Control attachedControl, string tooltipText)
        {
            var tooltip = new MetroToolTip
            {
                ShowAlways = true
            };

            tooltip.SetToolTip(attachedControl, tooltipText);

            return tooltip;
        }
    }
}