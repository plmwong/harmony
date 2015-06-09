namespace Harmony
{
    public interface IAuthorisationCodeDialogView
    {
        string AccessToken { get; }
        string PromptForAccessToken();
    }
}