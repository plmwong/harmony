namespace Harmony
{
    public interface ISettingsPresenter : IPresenter<ISettingsView>
    {
        void LoadSettings();
        void SaveSettings();
        void Show();
    }
}