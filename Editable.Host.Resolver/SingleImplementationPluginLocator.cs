using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Editable.Host.Infrastructure;
using Editable.Infrastructure;
using JetBrains.Annotations;

namespace Editable.Host.Resolver
{
    [UsedImplicitly]
    internal class SingleImplementationPluginLocator : IPluginLocator
    {
        private class ImplementationDescription
        {
            public Type Abstraction { get; }
            public Type Implementation { get; }

            public ImplementationDescription(Type abstraction, Type implementation)
            {
                Abstraction = abstraction;
                Implementation = implementation;
            }
        }

        private readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

        public void RegisterAssembly([NotNull] Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            var exportedImplementations = FetchAllPublicImplementations(assembly);
            foreach (var description in exportedImplementations)
            {
                _mappings[description.Abstraction] = description.Implementation;
            }
        }

        private IEnumerable<ImplementationDescription> FetchAllPublicImplementations(Assembly assembly) => 
            from type in assembly.GetTypes()
            where !type.IsAbstract && type.IsPublic
            let pluginContractType = type.GetInterfaces().FirstOrDefault(i => i.HasAttribute<PluginContractAttribute>())
            where pluginContractType != null
            select new ImplementationDescription(pluginContractType, type);

        public Type LocateImplementation([NotNull] Type pluginType)
        {
            if (pluginType == null)
            {
                throw new ArgumentNullException(nameof(pluginType));
            }
            if (!pluginType.HasAttribute<PluginContractAttribute>())
            {
                throw new ArgumentException("Requested type doesn't describe the plugin contract.", nameof(pluginType));
            }

            Type implementationType;
            if (_mappings.TryGetValue(pluginType, out implementationType))
            {
                return implementationType;
            }
            // check for open generic registration
            if (pluginType.IsConstructedGenericType)
            {
                var genericDefinition = pluginType.GetGenericTypeDefinition();
                if (_mappings.TryGetValue(genericDefinition, out implementationType))
                {
                    var typeParameters = pluginType.GetGenericArguments();
                    implementationType = implementationType.MakeGenericType(typeParameters);
                    return implementationType;
                }
            }
            return null;
        }
    }
}
