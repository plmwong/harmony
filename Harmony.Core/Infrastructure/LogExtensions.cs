namespace Harmony.Core.Infrastructure
{
    public static class LogExtensions
    {
        private const int IndentLength = 4;

        public static string IndentBy(this string message, int indentLevel)
        {
            var prefix = string.Empty;

            if (indentLevel > 0)
            {
                var emptySpaceAmount = (indentLevel - 1);
                var emptySpace = new string(' ', emptySpaceAmount*IndentLength);

                prefix = emptySpace + " -> ";
            }

            return prefix + message;
        }
    }
}