namespace Harmony.Core.Calendars
{
    public class SynchronisationResult
    {
        public int NumberCreated { get; private set; }
        public int NumberDeleted { get; private set; }

        public SynchronisationResult(int numberCreated, int numberDeleted)
        {
            NumberCreated = numberCreated;
            NumberDeleted = numberDeleted;
        }
    }
}