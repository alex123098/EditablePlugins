using System;
using System.Collections.Generic;
using System.Reflection;
using Editable.Host.Infrastructure;
using Editable.Host.Resolver.Marshaling;
using Editable.Host.Resolver.NullObject;
using Editable.Infrastructure;
using JetBrains.Annotations;

namespace Editable.Host.Resolver
{
    [UsedImplicitly]
    internal class PluginFactory : IPluginFactory
    {
        private readonly Dictionary<Type, AppDomainKeeper> _domainKeepers = new Dictionary<Type, AppDomainKeeper>();
        private readonly IPluginLocator _pluginLocator;
        private readonly IProxyTypeLocator _proxyTypeLocator;

        public PluginFactory(
            [NotNull] IPluginLocator pluginLocator,
            [NotNull] IProxyTypeLocator proxyTypeLocator)
        {
            if (pluginLocator == null)
            {
                throw new ArgumentNullException(nameof(pluginLocator));
            }
            if (proxyTypeLocator == null)
            {
                throw new ArgumentNullException(nameof(proxyTypeLocator));
            }
            _pluginLocator = pluginLocator;
            _proxyTypeLocator = proxyTypeLocator;
        }

        public IVoidMethodPlugin CreateVoidMethodPlugin()
        {
            var result = GetMethodPlugin(typeof (IVoidMethodPlugin));
            if (result == null)
            {
                return new DefaultVoidMethodPlugin();
            }
            return (IVoidMethodPlugin) result;
        }

        public IVoidMethodPlugin<T> CreateVoidMethodPlugin<T>()
        {
            var result = GetMethodPlugin(typeof(IVoidMethodPlugin<T>));
            if (result == null)
            {
                return new DefaultVoidMethodPlugin<T>();
            }
            return (IVoidMethodPlugin<T>) result;
        }

        public IMethodPlugin<TOut> CreateMethodPlugin<TOut>()
        {
            var result = GetMethodPlugin(typeof(IMethodPlugin<TOut>));
            if (result == null)
            {
                return new DefaultMethodPlugin<TOut>();
            }
            return (IMethodPlugin<TOut>) result;
        }

        public IMethodPlugin<TIn, TOut> CreateMethodPlugin<TIn, TOut>()
        {
            var result = GetMethodPlugin(typeof(IMethodPlugin<TIn, TOut>));
            if (result == null)
            {
                return new DefaultMethodPlugin<TIn, TOut>();
            }
            return (IMethodPlugin<TIn, TOut>)result;
        }

        private object GetMethodPlugin(Type pluginType)
        {
            var info = _pluginLocator.LocateImplementation(pluginType);
            if (info == null)
            {
                return null;
            }

            var domain = EnsureAppDomain(pluginType, info.AssemblyName);
            return domain.CreateInstanceAndUnwrap(
                typeof (IProxyTypeLocator).Assembly.FullName,
                _proxyTypeLocator.GetProxyType(pluginType).FullName,
                false,
                BindingFlags.Default,
                null,
                new object[] {info},
                null,
                null);
        }

        private AppDomain EnsureAppDomain(Type pluginType, string assemblyName)
        {
            AppDomainKeeper keeper;
            if (_domainKeepers.TryGetValue(pluginType, out keeper))
            {
                if (keeper.Domain.FriendlyName == assemblyName)
                {
                    return keeper.Domain;
                }
                keeper.Dispose();
            }
            _domainKeepers[pluginType] = new AppDomainKeeper(assemblyName);
            return _domainKeepers[pluginType].Domain;
        }
    }
}
