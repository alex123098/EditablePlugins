using System;
using System.Collections.Generic;
using System.Linq;
using Editable.Host.Infrastructure;
using Editable.Infrastructure;
using JetBrains.Annotations;

namespace Editable.Host.Resolver
{
    [UsedImplicitly]
    internal class SingleImplementationPluginLocator : IPluginLocator
    {
        private readonly PluginAssemblyDiscoverer _discoverer;
        private readonly Dictionary<string, ClassLocationInfo> _mappings = new Dictionary<string, ClassLocationInfo>();

        public SingleImplementationPluginLocator([NotNull] PluginAssemblyDiscoverer discoverer)
        {
            if (discoverer == null)
            {
                throw new ArgumentNullException(nameof(discoverer));
            }
            _discoverer = discoverer;
        }

        public void RegisterAssembly([NotNull] byte[] assemblyBytes)
        {
            if (assemblyBytes == null)
            {
                throw new ArgumentNullException(nameof(assemblyBytes));
            }
            var exportedImplementations = _discoverer.GetDeclaredMappings(assemblyBytes);
            foreach (var description in exportedImplementations)
            {
                _mappings[description.Abstraction] = new ClassLocationInfo(
                    description.AssemblyName,
                    description.Implementation,
                    assemblyBytes);
            }
        }

        public ClassLocationInfo LocateImplementation([NotNull] Type pluginType)
        {
            if (pluginType == null)
            {
                throw new ArgumentNullException(nameof(pluginType));
            }
            if (!pluginType.HasAttribute<PluginContractAttribute>())
            {
                throw new ArgumentException("Requested type doesn't describe the plugin contract.", nameof(pluginType));
            }

            ClassLocationInfo implementationInfo;
            if (_mappings.TryGetValue(pluginType.FullName, out implementationInfo))
            {
                return implementationInfo;
            }
            // check for open generic registration
            if (pluginType.IsConstructedGenericType)
            {
                var genericDefinition = pluginType.GetGenericTypeDefinition();
                if (_mappings.TryGetValue(genericDefinition.FullName, out implementationInfo))
                {
                    var typeParameters = pluginType.GetGenericArguments();
                    implementationInfo.GenericTypeParameters = typeParameters.Select(t => t.FullName).ToArray();
                    return implementationInfo;
                }
            }
            return null;
        }
    }
}
