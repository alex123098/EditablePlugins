using System;

namespace Editable.Host.Resolver.Marshaling
{
    internal interface IProxyTypeLocator
    {
        Type GetProxyType(Type requestedContract);
    }
}