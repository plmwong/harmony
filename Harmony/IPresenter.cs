namespace Harmony
{
    public interface IPresenter<out TView>
    {
         TView View { get; }
    }
}
