using Editable.Infrastructure;

namespace Editable.Host.Resolver.NullObject
{
    internal class DefaultMethodPlugin<TOut> : IMethodPlugin<TOut>
    {
        public TOut Invoke()
        {
            return default(TOut);
        }
    }

    internal class DefaultMethodPlugin<TIn, TOut> : IMethodPlugin<TIn, TOut>
    {
        public TOut Invoke(TIn arg)
        {
            return default(TOut);
        }
    }
}
