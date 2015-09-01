using System;
using Editable.Infrastructure;
using JetBrains.Annotations;

namespace Editable.Host.Resolver.Marshaling
{
    [UsedImplicitly]
    internal class ProxyTypeLocator : IProxyTypeLocator
    {
        public Type GetProxyType(Type requestedContract)
        {
            if (requestedContract == typeof (IVoidMethodPlugin))
            {
                return typeof (VoidMethodPluginDecorator);
            }
            if (requestedContract.GetGenericTypeDefinition() == typeof (IVoidMethodPlugin<>))
            {
                return typeof (VoidMethodPluginDecorator<>).MakeGenericType(requestedContract.GenericTypeArguments);
            }
            if (requestedContract.GetGenericTypeDefinition() == typeof(IMethodPlugin<>))
            {
                return typeof(MethodPluginDecorator<>).MakeGenericType(requestedContract.GenericTypeArguments);
            }
            if (requestedContract.GetGenericTypeDefinition() == typeof(IMethodPlugin<,>))
            {
                return typeof(MethodPluginDecorator<,>).MakeGenericType(requestedContract.GenericTypeArguments);
            }
            throw new NotSupportedException();
        }
    }
}
