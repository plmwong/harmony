namespace Harmony.Core.Infrastructure
{
    public static class StringExtensions
    {
        public static string FormatWith(this string format, params object[] formatObjects)
        {
            return string.Format(format, formatObjects);
        }
    }
}