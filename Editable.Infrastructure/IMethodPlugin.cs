namespace Editable.Infrastructure
{
    [PluginContract]
    public interface IMethodPlugin<out TOut>
    {
        TOut Invoke();
    }

    [PluginContract]
    public interface IMethodPlugin<in TIn, out TOut>
    {
        TOut Invoke(TIn arg);
    }
}
