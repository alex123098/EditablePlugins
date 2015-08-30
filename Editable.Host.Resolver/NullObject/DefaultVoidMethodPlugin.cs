using Editable.Infrastructure;

namespace Editable.Host.Resolver.NullObject
{
    internal class DefaultVoidMethodPlugin : IVoidMethodPlugin
    {
        public void Invoke()
        { }
    }

    internal class DefaultVoidMethodPlugin<T> : IVoidMethodPlugin<T>
    {
        public void Invoke(T arg)
        { }
    }
}
