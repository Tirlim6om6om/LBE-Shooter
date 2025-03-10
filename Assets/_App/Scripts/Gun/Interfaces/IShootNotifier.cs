namespace Tirlim.Gun
{
    public interface IShootNotifier
    {
        void OnShootMessage();
    }
    
    public interface IUnloadNotifier
    {
        void OnUnloadMessage();
    }
}