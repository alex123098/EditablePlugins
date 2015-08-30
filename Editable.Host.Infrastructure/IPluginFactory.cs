using Editable.Infrastructure;

namespace Editable.Host.Infrastructure
{
    public interface IPluginFactory
    {
        IVoidMethodPlugin CreateVoidMethodPlugin();

        IVoidMethodPlugin<T> CreateVoidMethodPlugin<T>();

        IMethodPlugin<TOut> CreateMethodPlugin<TOut>();

        IMethodPlugin<TIn, TOut> CreateMethodPlugin<TIn, TOut>();
    }
}
