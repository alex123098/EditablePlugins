using System;
using System.Reflection;
using Editable.Host.Infrastructure;
using Editable.Infrastructure;
using JetBrains.Annotations;

namespace Editable.Host.Resolver.Marshaling
{
    // This file contains definitions of decorators which are used as the transparent proxies for calls accross appDomain boundaries

    internal class VoidMethodPluginDecorator : MarshalByRefObject, IVoidMethodPlugin
    {
        private readonly IVoidMethodPlugin _actualPlugin;

        public VoidMethodPluginDecorator([NotNull] ClassLocationInfo locationInfo)
        {
            if (locationInfo == null)
            {
                throw new ArgumentNullException(nameof(locationInfo));
            }
            var assembly = Assembly.Load(locationInfo.AssemblyBytes);
            _actualPlugin = (IVoidMethodPlugin) assembly.CreateInstance(locationInfo.TypeName);
        }

        public void Invoke()
        {
            _actualPlugin.Invoke();
        }
    }

    internal class VoidMethodPluginDecorator<T> : MarshalByRefObject, IVoidMethodPlugin<T>
    {
        private readonly IVoidMethodPlugin<T> _actualPlugin;

        public VoidMethodPluginDecorator([NotNull] ClassLocationInfo locationInfo)
        {
            if (locationInfo == null)
            {
                throw new ArgumentNullException(nameof(locationInfo));
            }
            var assembly = Assembly.Load(locationInfo.AssemblyBytes);
            _actualPlugin = (IVoidMethodPlugin<T>) assembly.CreateInstance(locationInfo.TypeName);
        }

        public void Invoke(T arg)
        {
            _actualPlugin.Invoke(arg);
        }
    }
}
