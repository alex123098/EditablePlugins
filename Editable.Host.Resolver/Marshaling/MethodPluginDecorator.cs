using System;
using System.Reflection;
using Editable.Host.Infrastructure;
using Editable.Infrastructure;
using JetBrains.Annotations;

namespace Editable.Host.Resolver.Marshaling
{
    // This file contains definitions of decorators which are used as the transparent proxies for calls accross appDomain boundaries

    internal class MethodPluginDecorator<TOut> : MarshalByRefObject, IMethodPlugin<TOut>
    {
        private readonly IMethodPlugin<TOut> _actualPlugin;

        public MethodPluginDecorator([NotNull] ClassLocationInfo locationInfo)
        {
            if (locationInfo == null)
            {
                throw new ArgumentNullException(nameof(locationInfo));
            }
            var assembly = Assembly.Load(locationInfo.AssemblyBytes);
            _actualPlugin = (IMethodPlugin<TOut>) assembly.CreateInstance(locationInfo.TypeName);
        } 

        public TOut Invoke()
        {
            return _actualPlugin.Invoke();
        }
    }

    internal class MethodPluginDecorator<TIn, TOut> : MarshalByRefObject, IMethodPlugin<TIn, TOut>
    {
        private readonly IMethodPlugin<TIn, TOut> _actualPlugin;

        public MethodPluginDecorator([NotNull] ClassLocationInfo locationInfo)
        {
            if (locationInfo == null)
            {
                throw new ArgumentNullException(nameof(locationInfo));
            }
            var assembly = Assembly.Load(locationInfo.AssemblyBytes);
            _actualPlugin = (IMethodPlugin<TIn, TOut>) assembly.CreateInstance(locationInfo.TypeName);
        }

        public TOut Invoke(TIn arg)
        {
            return _actualPlugin.Invoke(arg);
        }
    }
}
