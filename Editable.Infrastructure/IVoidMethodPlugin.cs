namespace Editable.Infrastructure
{
    [PluginContract]
    public interface IVoidMethodPlugin
    {
        void Invoke();
    }

    [PluginContract]
    public interface IVoidMethodPlugin<in TIn>
    {
        void Invoke(TIn arg);
    }
}
