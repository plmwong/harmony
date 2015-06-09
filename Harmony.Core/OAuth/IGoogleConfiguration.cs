namespace Harmony.Core.OAuth
{
    public interface IGoogleConfiguration
    {
        string GoogleRefreshToken { get; set; }
        string GoogleCalendarId { get; set; }
    }
}